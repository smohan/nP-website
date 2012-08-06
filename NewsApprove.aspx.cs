using System;

public partial class NewsApprove : System.Web.UI.Page
{
    private const string NewArticlePath = "~/new-news";

    private NewsRepository _articleRepository;

    private NewsRepository ArticleRepository
    {
        get
        {
            return _articleRepository ?? (_articleRepository = new NewsRepository(NewArticlePath));
        }
    }

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

    protected void cmdApprove_Click(object sender, EventArgs e)
    {
        ArticleRepository.CopyTo("~/news");
    }
}