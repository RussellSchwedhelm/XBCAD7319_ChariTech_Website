using System;
using System.IO; // For logging
using System.Linq;
using System.Web;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class GoogleCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Get the authenticated user from OWIN context
                var user = HttpContext.Current.GetOwinContext().Authentication.User;

                if (user.Identity.IsAuthenticated)
                {
                    // Safely extract the email claim
                    var emailClaim = user.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email);
                    string email = emailClaim?.Value ?? string.Empty;

                    // Safely extract the first name claim
                    var firstNameClaim = user.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.GivenName);
                    string firstName = firstNameClaim?.Value ?? string.Empty;

                    // Safely extract the surname claim
                    var surnameClaim = user.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Surname);
                    string surname = surnameClaim?.Value ?? string.Empty;

                    // Safely extract the profile picture claim (if available)
                    var profilePictureClaim = user.Claims.FirstOrDefault(c => c.Type == "picture");
                    string profilePictureUrl = profilePictureClaim?.Value ?? string.Empty;

                    // Safely extract the Google User Identifier (sub claim)
                    var subClaim = user.Claims.FirstOrDefault(c => c.Type == "sub");
                    string googleUserId = subClaim?.Value ?? string.Empty;

                    // Check if email is valid and mandatory
                    if (string.IsNullOrEmpty(email))
                    {
                        // Redirect to an error page with a user-friendly message
                        Response.Redirect("Error.aspx?message=Email not found in Google authentication.");
                        return;
                    }

                    // Check if the user is already registered
                    RegistrationManager registrationManager = new RegistrationManager();
                    if (registrationManager.IsEmailRegistered(email))
                    {
                        // Log the user in if already registered
                        Session["UserEmail"] = email;
                        Session["GoogleUserId"] = googleUserId; // Store Google user ID
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        // Store the user's OAuth data in session and redirect to the registration page
                        Session["OAuthEmail"] = email;
                        Session["OAuthFirstName"] = firstName;
                        Session["OAuthSurname"] = surname;
                        Session["OAuthProfilePicture"] = profilePictureUrl; // Store profile picture URL
                        Session["GoogleUserId"] = googleUserId; // Store Google user ID
                        Response.Redirect("Register.aspx");
                    }
                }
                else
                {
                    // Handle the case where authentication failed
                    Response.Write("<script>alert('Google authentication failed');</script>");
                    Response.Redirect("Login.aspx");
                }
            }
            catch (Exception ex)
            {
                // Temporarily display error details in the browser for debugging purposes
                Response.Write("<pre>" + ex.ToString() + "</pre>");
                // You can still log the error if logging works
                LogError(ex);
            }

        }

        // Error logging method
        private void LogError(Exception ex)
        {
            // Define the path where logs will be saved
            string logDirectory = Server.MapPath("~/ErrorLogs/");
            string logFilePath = Path.Combine(logDirectory, "log.txt");

            // Ensure the directory exists
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Log the exception details to the file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine("Error occurred on: " + DateTime.Now);
                writer.WriteLine("Message: " + ex.Message);
                writer.WriteLine("Stack Trace: " + ex.StackTrace);
                writer.WriteLine("----------------------------------------");
            }
        }

    }
}
