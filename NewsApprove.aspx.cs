using System;

public partial class NewsApprove : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadArticles();
        }
    }

    private void LoadArticles()
    {
        this.dataArticles.DataSource = AppState.PendingNews.GetAllItems();
        DataBind();
    }

    protected void cmdApprove_Click(object sender, EventArgs e)
    {
        AppState.PendingNews.CopyTo(AppState.CurrentNews);
        Response.Redirect("News.aspx");
    }
}