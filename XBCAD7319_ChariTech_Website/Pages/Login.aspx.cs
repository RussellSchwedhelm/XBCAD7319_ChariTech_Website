using System;
using System.Linq;
using System.Web;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] != null)
            {
                // If the user is already logged in, redirect to the dashboard
                Response.Redirect("Home.aspx");
            }

            // Check if the user is coming from an OAuth provider (Google or Facebook)
            var user = HttpContext.Current.GetOwinContext().Authentication.User;

            if (user.Identity.IsAuthenticated)
            {
                // Extract user details from OAuth claims
                var emailClaim = user.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email);
                string email = emailClaim?.Value;

                // Create session and redirect if the user is authenticated via OAuth
                if (!string.IsNullOrEmpty(email))
                {
                    Session["UserEmail"] = email;
                    Session.Timeout = 30;
                    Response.Redirect("Home.aspx"); // Redirect to Home page after login
                }
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];

            LoginManager loginManager = new LoginManager();
            bool isAuthenticated = loginManager.AuthenticateUser(email, password);

            if (isAuthenticated)
            {
                int userRoleID = (int)Session["UserRoleID"];

                // Redirect based on role
                if (userRoleID == 2) // Admin Role
                {
                    Response.Redirect("/Pages/AdminDashboard.aspx");
                }
                else
                {
                    Response.Redirect("/Pages/Home.aspx");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid email or password. Please try again.');</script>");
            }
        }

    }
}
