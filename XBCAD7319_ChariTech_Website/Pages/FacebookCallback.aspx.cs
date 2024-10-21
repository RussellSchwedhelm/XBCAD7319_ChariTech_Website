using System;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class FacebookCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = HttpContext.Current.GetOwinContext().Authentication.User;

            if (user.Identity.IsAuthenticated)
            {
                var emailClaim = user.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email);
                string email = emailClaim?.Value;

                var firstNameClaim = user.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.GivenName);
                string firstName = firstNameClaim?.Value;

                var surnameClaim = user.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Surname);
                string surname = surnameClaim?.Value;

                var profilePictureClaim = user.Claims.FirstOrDefault(c => c.Type == "picture");
                string profilePictureUrl = profilePictureClaim?.Value;

                // Check if the user is already registered
                RegistrationManager registrationManager = new RegistrationManager();
                if (registrationManager.IsEmailRegistered(email))
                {
                    // Log the user in if already registered
                    Session["UserEmail"] = email;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    // Store the data in session and redirect to the registration page
                    Session["OAuthEmail"] = email;
                    Session["OAuthFirstName"] = firstName;
                    Session["OAuthSurname"] = surname;
                    Session["OAuthProfilePicture"] = profilePictureUrl; // Store profile picture URL
                    Response.Redirect("Register.aspx");
                }
            }
            else
            {
                Response.Write("<script>alert('Facebook authentication failed');</script>");
                Response.Redirect("Login.aspx");
            }
        }
    }
}
