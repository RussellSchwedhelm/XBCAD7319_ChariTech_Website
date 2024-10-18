using System;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            // Retrieve the form inputs
            string firstName = Request.Form["firstName"];
            string surname = Request.Form["surname"];
            string email = Request.Form["email"];
            string ecclesia = Request.Form["ecclesia"];
            string password = Request.Form["password"];
            string rePassword = Request.Form["rePassword"];

            // Validate that both passwords match
            if (password != rePassword)
            {
                Response.Write("<script>alert('Passwords do not match!');</script>");
                return;
            }

            // Create a new instance of the RegistrationManager
            RegistrationManager registrationManager = new RegistrationManager();
            bool isRegistered = registrationManager.RegisterUser(firstName, surname, email, ecclesia, password);

            // Handle the result of the registration
            if (isRegistered)
            {
                // Registration was successful
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Registration failed
                Response.Write("<script>alert('Registration failed. Email may already be in use.');</script>");
            }
        }
    }
}
