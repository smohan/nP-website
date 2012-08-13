using System;
using System.Web.UI;

public partial class News : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var articles = AppState.CurrentNews.GetAllItems();

        this.dataArticles.DataSource = articles;
        DataBind();
    }

}