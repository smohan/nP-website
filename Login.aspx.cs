using System;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void cmdLogin_Click(object sender, EventArgs e)
    {
        if (FormsAuthentication.Authenticate(inputUser.Text, inputPassword.Text))
        {
            FormsAuthentication.RedirectFromLoginPage(inputUser.Text, true);
        }
    }
}