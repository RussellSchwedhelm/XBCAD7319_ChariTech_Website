using System;
using System.Web.UI;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Get email and password input
            string email = Request.Form["email"];
            string password = Request.Form["password"];

            // Initialize the LoginManager and authenticate
            LoginManager loginManager = new LoginManager();
            bool isAuthenticated = loginManager.AuthenticateUser(email, password);

            if (isAuthenticated)
            {
                // Create a session and redirect to the protected page
                Session["UserEmail"] = email;
                Response.Redirect("Dashboard.aspx");  // Example redirection after successful login
            }
            else
            {
                // Display error message
                Response.Write("<script>alert('Invalid login credentials');</script>");
            }
        }
    }
}
