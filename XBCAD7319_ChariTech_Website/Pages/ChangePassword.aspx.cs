using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class ChangePassword : Page
    {
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSettings.aspx"); //I will remove this once database is up
            // Retrieve the entered passwords
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Server-side validation for password match
            if (newPassword != confirmPassword)
            {
                // Display the error message
                passwordError.InnerText = "Passwords don't match";
                passwordError.Style["display"] = "block";
            }
            else
            {
                // Code to update the password in the database
                // UpdatePassword(newPassword); // Assume a method to update password in database

                // Redirect to the UserSettings page upon successful password change
                Response.Redirect("UserSettings.aspx");
            }
        }
    }
}
