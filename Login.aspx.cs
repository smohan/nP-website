using System;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void cmdLogin_Click(object sender, EventArgs e)
    {
		if (IsValid)
		{
			FormsAuthentication.RedirectFromLoginPage(inputUser.Text, true);
		}
    }

	protected void ValidateAuthentication(object source, ServerValidateEventArgs args)
	{
		args.IsValid = FormsAuthentication.Authenticate(inputUser.Text, inputPassword.Text);
	}
}