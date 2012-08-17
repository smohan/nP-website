using System;
using System.Collections.Generic;
using System.Web.UI;

public partial class News : Page
{
    protected string Id { get; private set; }

    protected bool IsSingleItem { get { return !string.IsNullOrEmpty(Id); } }

	protected bool SingleItemNotFound { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Id = Request["id"];
        IList<NewsItem> articles;
		if (IsSingleItem)
		{
			var item = AppState.CurrentNews.GetItemById(Id);
			if (null == item)
			{
				articles = new NewsItem[0];
				SingleItemNotFound = true;
			}
			else
				articles = new[] { item };
		}
		else
			articles = AppState.CurrentNews.GetAllItems();

	    this.dataArticles.DataSource = articles;
        DataBind();
    }
}