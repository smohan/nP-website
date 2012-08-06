using System;
using System.Web.UI;

public partial class News : Page
{
    private const string ArticlePath = "~/news";

    protected void Page_Load(object sender, EventArgs e)
    {
        var articles = new NewsRepository(ArticlePath).GetAllItems();

        this.dataArticles.DataSource = articles;
        DataBind();
    }

}