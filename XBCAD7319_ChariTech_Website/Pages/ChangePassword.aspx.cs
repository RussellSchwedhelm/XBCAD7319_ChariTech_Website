using System;
using System.Security.Cryptography;
using System.Text; 
using System.Web.UI;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class ChangePassword : Page
    {
        // Read-only instance of UserPreferenceDAL for database access, initialized only once
        private readonly UserPreferenceDAL userPreferenceDAL = new UserPreferenceDAL();

        // Page_Load method is called when the page is initially loaded or on postbacks
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user session contains a valid UserEmail (indicating the user is logged in)
            /*if (Session["UserEmail"] == null)
            {
                // Redirect the user to the Login page if no UserEmail session exists
                Response.Redirect("Login.aspx");
            }
            // If this is the first time the page is loaded (not a postback)
            if (!IsPostBack)
            {
                // This block is empty and reserved for any actions needed only on initial load
            }*/
        }

        // Event handler for Cancel button click; redirects user to UserSettings page
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSettings.aspx");
        }

        // Event handler for Change Password button click
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Retrieve new and confirm password values entered by the user, trimming extra spaces
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Check if the new password matches the confirm password
            if (newPassword != confirmPassword)
            {
                // Display error message if passwords don't match
                passwordError.InnerText = "Passwords don't match";
                passwordError.Visible = true;
                return; // Exit method early if passwords don't match
            }

            // Hash the new password for secure storage in the database
            string hashedPassword = HashPassword(newPassword);

            // Update the hashed password in the database using the UserEmail from the session
            if (userPreferenceDAL.UpdatePasswordHash(Session["UserEmail"].ToString(), hashedPassword))
            {
                // Show success message and redirect user to UserSettings page
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('Password changed successfully!'); window.location.href='UserSettings.aspx';",
                    true
                );
            }
            else
            {
                // Display error message if password update fails
                passwordError.InnerText = "An error occurred. Please try again.";
                passwordError.Visible = true;
            }
        }

        // Private method for hashing the password using SHA256 for security
        private string HashPassword(string password)
        {
            // Create a new SHA256 object for password hashing
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert password string to byte array, hash it, and store the result in 'bytes'
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Use StringBuilder to build the hexadecimal representation of the hashed password
                StringBuilder builder = new StringBuilder();
                // Loop through each byte in the hashed password
                foreach (byte b in bytes)
                {
                    // Append each byte as a two-character hexadecimal string to the builder
                    builder.Append(b.ToString("x2"));
                }
                // Return the final hexadecimal string representation of the hashed password
                return builder.ToString();
            }
        }
    }
}
