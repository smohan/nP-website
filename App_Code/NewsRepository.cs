using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public class NewsRepository
{
    private const int NewsCountMagnitude = 2;
    private readonly DirectoryInfo _newsDirectory;
    private static readonly string NewsItemIndexPattern = @"(?<Index>" + string.Concat(Enumerable.Repeat(@"\d", NewsCountMagnitude).ToArray()) + ")";
    private static readonly string NewsItemNamePattern = @"news" + NewsItemIndexPattern;
    private static readonly Regex NewsItemNameExpression = new Regex(NewsItemNamePattern);
    private static readonly string NewsItemIndexFormat = new string('0', NewsCountMagnitude);
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
        return (from file in GetNewsFiles()
                select GetItemByFileName(file.FullName)).ToList();
    }

    public NewsItem GetItemById(string id)
    {
        var fileName = GetFileNameFromId(id);
        return GetItemByFileName(fileName);
    }

    private string GetFileNameFromId(string id)
    {
        var fileName = Path.ChangeExtension(Path.Combine(_newsDirectory.FullName, id), NewsItemExtension);
        return fileName;
    }

    public void SaveItem(NewsItem item)
    {
        if(string.IsNullOrEmpty(item.Source))
        {
            item.Source = GetNextItemId();
        }
        var releaseDateString = item.ReleaseDate.ToString(ReleaseDateFormat, CultureInfo.InvariantCulture);
        File.WriteAllLines(GetFileNameFromId(item.Source), new []{item.Title, releaseDateString, item.Content});
    }

    public void DeleteItem(string id)
    {
        File.Delete(GetFileNameFromId(id));
    }

    public void MoveLater(string id)
    {
        var itemIndex = GetItemIndexFromId(id);
        var newItemIndex = itemIndex - 1;
        var newId = GetItemIdFromIndex(newItemIndex);
        var oldFileName = GetFileNameFromId(id);
        var newFileName = GetFileNameFromId(newId);
        var tempFileName = Path.GetTempFileName();
        File.Delete(tempFileName);
        File.Move(newFileName, tempFileName);
        File.Move(oldFileName, newFileName);
        File.Move(tempFileName, oldFileName);
    }

    private IEnumerable<FileInfo> GetNewsFiles()
    {
        return from file in _newsDirectory.GetFiles("*." + NewsItemExtension)
               where NewsItemNameExpression.IsMatch(GetIdFromFileName(file.Name))
               orderby file.Name descending
               select file;
    }

    private static NewsItem GetItemByFileName(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        return new NewsItem
            {
                Source = GetIdFromFileName(fileName),
                Title = lines[0],
                ReleaseDate = DateTime.ParseExact(lines[1], ReleaseDateFormat, CultureInfo.InvariantCulture),
                Content = lines.Skip(2).Aggregate((x, y) => x + y)
            };
    }

    private string GetNextItemId()
    {
        var lastItem = GetNewsFiles().FirstOrDefault();
        var lastItemIndex = 0;
        if(null != lastItem)
        {
            lastItemIndex = GetItemIndexFromId(GetIdFromFileName(lastItem.Name));
        }
        var nextItemIndex = lastItemIndex + 1;
        return GetItemIdFromIndex(nextItemIndex);
    }

    private static string GetItemIdFromIndex(int index)
    {
        return NewsItemNamePattern.Replace(NewsItemIndexPattern, index.ToString(NewsItemIndexFormat));
    }

    private static int GetItemIndexFromId(string id)
    {
        return int.Parse(NewsItemNameExpression.Match(id).Groups[1].Value);
    }

    private static string GetIdFromFileName(string fileName)
    {
        return Path.GetFileNameWithoutExtension(fileName);
    }

    public void CopyTo(string newVirtualPath)
    {
        var tempDirectoryPath = Path.GetTempFileName();
        File.Delete(tempDirectoryPath);
        var tempDirectory = new DirectoryInfo(tempDirectoryPath);
        tempDirectory.Create();
        foreach(var file in GetNewsFiles())
        {
            file.CopyTo(Path.Combine(tempDirectoryPath, file.Name));
        }
        var newPhysicalPath = GetPhysicalFromVirtualPath(newVirtualPath);
        Directory.Delete(newPhysicalPath, true);
        Directory.Move(tempDirectoryPath, newPhysicalPath);
    }
}