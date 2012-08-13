using System;
using System.Web.UI.WebControls;

public partial class NewsEdit : System.Web.UI.Page
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

    protected void cmdDelete_Click(object sender, CommandEventArgs e)
    {
        AppState.PendingNews.DeleteItem((string)e.CommandArgument);
        LoadArticles();
    }

    protected void cmdSwitch_Click(object sender, CommandEventArgs e)
    {
        AppState.PendingNews.MoveLater((string) e.CommandArgument);
        LoadArticles();
    }

}
