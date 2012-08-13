using System;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class ArticleEdit : System.Web.UI.Page
{
    private const string ReleaseDateFormat = "yyyy-MM-dd";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            var source = Request.Params["Source"];
            if (null != source)
            {
                this.Source = source;
                var article = AppState.PendingNews.GetItemById(source);
                this.inputTitle.Text = article.Title;
                this.inputContent.Text = article.Content;
                this.inputReleaseDate.Text = article.ReleaseDate.ToString(ReleaseDateFormat);
            }
        }
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            AppState.PendingNews.SaveItem(new NewsItem
                {
                    Source = Source,
                    Title = this.inputTitle.Text,
                    ReleaseDate = ParseReleaseDate().Value,
                    Content = this.inputContent.Text
                });
            Response.Redirect("NewsEdit.aspx");
        }
    }

    private string Source
    {
        get { return (string)ViewState["Source"]; }
        set { ViewState["Source"] = value; }
    }

    protected void validatorReleaseDate_Validate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = ParseReleaseDate().HasValue;
    }

    private DateTime? ParseReleaseDate()
    {
        DateTime result;
        return DateTime.TryParseExact(this.inputReleaseDate.Text, ReleaseDateFormat, CultureInfo.CurrentUICulture,
            DateTimeStyles.None, out result) ? result : new DateTime?();
    }
}