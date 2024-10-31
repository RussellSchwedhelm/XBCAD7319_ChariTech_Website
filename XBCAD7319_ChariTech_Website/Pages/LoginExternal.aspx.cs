using Microsoft.Owin.Security;
using System;
using System.Web;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class LoginExternal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the provider from the query string
            string provider = Request.QueryString["provider"];

            // Check if the provider parameter is not empty or null
            if (!string.IsNullOrEmpty(provider))
            {
                // Convert the provider to lowercase for consistent comparison
                provider = provider.ToLower();

                // Switch based on the provider
                switch (provider)
                {
                    case "google":
                        // Trigger Google authentication challenge
                        HttpContext.Current.GetOwinContext().Authentication.Challenge(
                            new AuthenticationProperties { RedirectUri = "https://localhost:44366/Pages/GoogleCallback.aspx" }, "Google");
                        break;

                    case "facebook":
                        // Trigger Facebook authentication challenge
                        HttpContext.Current.GetOwinContext().Authentication.Challenge(
                            new AuthenticationProperties { RedirectUri = "https://localhost:44366/Pages/FacebookCallback.aspx" }, "Facebook");
                        break;

                    default:
                        // Handle unsupported or unknown providers
                        Response.Write("<script>alert('Unknown authentication provider.');</script>");
                        break;
                }
            }
            else
            {
                // Handle missing provider parameter
                Response.Write("<script>alert('No authentication provider specified.');</script>");
            }
        }
    }
}
