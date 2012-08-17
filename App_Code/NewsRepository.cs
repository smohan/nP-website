using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

public class NewsRepository
{
    private readonly DirectoryInfo _newsDirectory;
    private const string NewsItemExtension = "txt";
    private const string ReleaseDateFormat = "yyyy-MM-dd";

    public NewsRepository(string newsVirtualPath)
    {
        _newsDirectory = new DirectoryInfo(GetPhysicalFromVirtualPath(newsVirtualPath));
    }

    private static string GetPhysicalFromVirtualPath(string newsVirtualPath)
    {
        return HttpContext.Current.Server.MapPath(newsVirtualPath);
    }

    public IList<NewsItem> GetAllItems()
    {
        return (from file in GetNewsFileNames()
                select GetItemByFileName(file)).ToList();
    }

    public NewsItem GetItemById(string id)
    {
        var fileName = GetFileNameFromId(id);
        return null == fileName ? null : GetItemByFileName(fileName);
    }

    public void SaveItem(NewsItem item)
    {
        var fileName = string.IsNullOrEmpty(item.Id)
                           ? new NewsItemFileName(GetNewItemId(), GetNextItemIndex())
                           : GetFileNameFromId(item.Id);

        var releaseDateString = item.ReleaseDate.ToString(ReleaseDateFormat, CultureInfo.InvariantCulture);
        File.WriteAllLines(GetFullFileName(fileName), new[] {item.Title, releaseDateString, item.Content});
    }

    private string GetFullFileName(NewsItemFileName fileName)
    {
        return Path.Combine(_newsDirectory.FullName, Path.ChangeExtension(fileName.FileName, NewsItemExtension));
    }

    private static string GetNewItemId()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("/", "_")
            .Replace("+", "-")
            .Substring(0, 22);
    }

    public void DeleteItem(string id)
    {
        var fileName = GetFileNameFromId(id);
        GetFileInfo(fileName).Delete();
        var index = fileName.Index;
        while(ItemAtIndexExists(++index))
        {
            GetFileInfo(GetFileNameFromIndex(index)).MoveTo(GetFullFileName(GetFileNameFromIndex(index - 1)));
        }
    }

    private FileInfo GetFileInfo(NewsItemFileName fileName)
    {
        return new FileInfo(GetFullFileName(fileName));
    }

    private bool ItemAtIndexExists(int index)
    {
        return GetNewsFileNames().Any(fn => fn.Index == index);
    }

    private NewsItemFileName GetFileNameFromIndex(int index)
    {
        return GetNewsFileNames().SingleOrDefault(fn => fn.Index == index);
    }

    private NewsItemFileName GetFileNameFromId(string id)
    {
        return GetNewsFileNames().SingleOrDefault(fn => fn.Id == id);
    }

    public void MoveLater(string id)
    {
        var oldItemOldName = GetFileNameFromId(id);
        var oldItemIndex = oldItemOldName.Index;
        var newItemIndex = oldItemIndex - 1;
        var oldItemNewName = new NewsItemFileName(id, newItemIndex);
        var newItemOldName = GetFileNameFromIndex(newItemIndex);
        var newItemNewName = new NewsItemFileName(newItemOldName.Id, oldItemIndex);
        var tempFileName = Path.GetTempFileName();
        var newItemOldInfo = GetFileInfo(newItemOldName);
        var oldFileOldInfo = GetFileInfo(oldItemOldName);
        var tempFileInfo = new FileInfo(tempFileName);
        tempFileInfo.Delete();
        newItemOldInfo.MoveTo(tempFileName);
        oldFileOldInfo.MoveTo(GetFileInfo(oldItemNewName).FullName);
        tempFileInfo.MoveTo(GetFileInfo(newItemNewName).FullName);
    }

    private IEnumerable<NewsItemFileName> GetNewsFileNames()
    {
        return from file in _newsDirectory.GetFiles("*." + NewsItemExtension)
               let fileName = NewsItemFileName.TryParse(file.Name)
               where null != fileName
               orderby fileName.FileName descending
               select fileName;
    }

    private NewsItem GetItemByFileName(NewsItemFileName fileName)
    {
        var fullFileName = GetFullFileName(fileName);
        var lines = File.ReadAllLines(fullFileName);
        return new NewsItem
            {
                Id = fileName.Id,
                Title = lines[0],
                ReleaseDate = DateTime.ParseExact(lines[1], ReleaseDateFormat, CultureInfo.InvariantCulture),
                Content = lines.Skip(2).Aggregate((x, y) => x + y)
            };
    }

    private int GetNextItemIndex()
    {
        var lastItem = GetNewsFileNames().FirstOrDefault();
        var lastItemIndex = 0;
        if(null != lastItem)
        {
            lastItemIndex = lastItem.Index;
        }
        var nextItemIndex = lastItemIndex + 1;
        return nextItemIndex;
    }

    public void CopyTo(NewsRepository targetRepository)
    {
        var targetNewsDirectory = targetRepository._newsDirectory;
        CopyTo(targetNewsDirectory);
    }

    private void CopyTo(DirectoryInfo targetNewsDirectory)
    {
        var tempDirectoryPath = Path.GetTempFileName();
        File.Delete(tempDirectoryPath);
        var tempDirectory = new DirectoryInfo(tempDirectoryPath);
        tempDirectory.Create();
        foreach (var file in GetNewsFileNames())
        {
            GetFileInfo(file).CopyTo(Path.Combine(tempDirectoryPath, file.FileName));
        }
        targetNewsDirectory.Delete(true);
        tempDirectory.MoveTo(targetNewsDirectory.FullName);
    }
}