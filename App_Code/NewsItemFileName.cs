using System;
using System.Linq;
using System.Text.RegularExpressions;

public class NewsItemFileName
{
    private const int NewsCountMagnitude = 2;
    private static readonly string NewsItemIndexPattern = @"(?<Index>" + string.Concat(Enumerable.Repeat(@"\d", NewsCountMagnitude).ToArray()) + ")";
    private static readonly string NewsItemIdPattern = @"(?<Id>\w{20})";
    private static readonly string NewsItemNamePattern = @"news" + NewsItemIndexPattern + "-" + NewsItemIdPattern;
    private static readonly Regex NewsItemNameExpression = new Regex(NewsItemNamePattern);
    private static readonly string NewsItemIndexFormat = new string('0', NewsCountMagnitude);

    private readonly string _fileName;
    private readonly string _id;
    private readonly int _index;

    private static Match MatchFileName(string fileName)
    {
        return NewsItemNameExpression.Match(fileName);
    }

    public NewsItemFileName(string fileName)
        : this(fileName, MatchFileName(fileName))
    {
    }

    public NewsItemFileName(string id, int index)
    {
        _id = id;
        _index = index;
        _fileName = NewsItemNamePattern.Replace(NewsItemIdPattern, _id).Replace(NewsItemIndexPattern, index.ToString(NewsItemIndexFormat));
    }

    private NewsItemFileName(string fileName, Match match)
    {
        if (!match.Success)
        {
            throw new ArgumentException("FileName '" + fileName + "' does not match the expected pattern.");
        }
        _fileName = fileName;
        _id = match.Groups["Id"].Value;
        _index = int.Parse(match.Groups["Index"].Value);
    }

    public string Id
    {
        get { return _id; }
    }

    public int Index
    {
        get { return _index; }
    }

    public string FileName
    {
        get { return _fileName; }
    }

    public static NewsItemFileName TryParse(string fileName)
    {
        var match = MatchFileName(fileName);
        return match.Success ? new NewsItemFileName(fileName, match) : null;
    }
}