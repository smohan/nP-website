using System;
using System.Web.UI.WebControls;

public partial class NewsEdit : System.Web.UI.Page
{
    private const string NewArticlePath = "~/new-news";

    private NewsRepository _articleRepository;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadArticles();
        }
    }

    private void LoadArticles()
    {
        this.dataArticles.DataSource = ArticleRepository.GetAllItems();
        DataBind();
    }

    protected void cmdDelete_Click(object sender, CommandEventArgs e)
    {
        ArticleRepository.DeleteItem((string)e.CommandArgument);
        LoadArticles();
    }

    private NewsRepository ArticleRepository
    {
        get
        {
            return _articleRepository ?? (_articleRepository = new NewsRepository(NewArticlePath));            
        }
    }

    protected void cmdSwitch_Click(object sender, CommandEventArgs e)
    {
        ArticleRepository.MoveLater((string) e.CommandArgument);
        LoadArticles();
    }

}
