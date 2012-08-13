<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        AppState.CurrentNews = new NewsRepository(ConfigurationManager.AppSettings["CurrentNewsVirtualPath"]);
        AppState.PendingNews = new NewsRepository(ConfigurationManager.AppSettings["PendingNewsVirtualPath"]);
    }    
       
</script>
