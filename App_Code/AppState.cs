using System.Web;

public static class AppState
{
    private const string CurrentNewsKey = "CurrentNews";
    private const string PendingNewsKey = "PendingNews";

    public static NewsRepository CurrentNews
    {
        get { return (NewsRepository) HttpContext.Current.Application[CurrentNewsKey]; }
        set { HttpContext.Current.Application[CurrentNewsKey] = value; }
    }

    public static NewsRepository PendingNews
    {
        get { return (NewsRepository) HttpContext.Current.Application[PendingNewsKey]; }
        set { HttpContext.Current.Application[PendingNewsKey] = value; }
    }
}