using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Prefill the form if the user is coming from Google or Facebook OAuth
                if (Session["OAuthEmail"] != null)
                {
                    emailInput.Text = Session["OAuthEmail"].ToString();
                    firstNameInput.Text = Session["OAuthFirstName"]?.ToString() ?? string.Empty;
                    surnameInput.Text = Session["OAuthSurname"]?.ToString() ?? string.Empty;

                    // Display profile picture if available
                    if (Session["OAuthProfilePicture"] != null)
                    {
                        profilePicture.ImageUrl = Session["OAuthProfilePicture"].ToString();
                        profilePicture.Visible = true;
                    }

                    // Optionally, hide the password fields for SSO users since they don't need to set a password
                    passwordContainer.Visible = false;
                    rePasswordContainer.Visible = false;
                }

                // Populate ecclesia drop-down with church names
                PopulateEcclesiaDropdown();
            }
        }

        // Method to populate the ecclesia drop-down list
        private void PopulateEcclesiaDropdown()
        {
            RegistrationManager registrationManager = new RegistrationManager();
            DataTable churches = registrationManager.GetSortedChurches();

            // Clear any existing items in the drop-down list
            ecclesia.Items.Clear();

            // Add a default "Select" option
            ecclesia.Items.Add(new ListItem("Select", ""));

            // Add church items from the fetched list
            foreach (DataRow row in churches.Rows)
            {
                ecclesia.Items.Add(new ListItem(row["ChurchName"].ToString(), row["ChurchID"].ToString()));
            }
        }


        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            // Collect user input
            string firstName = firstNameInput.Text.Trim();
            string surname = surnameInput.Text.Trim();
            string email = emailInput.Text.Trim();
            string password = passwordInput.Text.Trim();
            int churchID = -1;
            byte[] profilePicture = null;

            // *** VALIDATION ***

            // Validate first name and surname
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
            {
                Response.Write("<script>alert('Please fill in both First Name and Surname.');</script>");
                return;
            }

            // Validate email format
            if (!IsValidEmail(email))
            {
                Response.Write("<script>alert('Please enter a valid email address.');</script>");
                return;
            }

            // Validate ecclesia selection
            if (string.IsNullOrEmpty(ecclesia.SelectedValue) || ecclesia.SelectedValue == "Select")
            {
                Response.Write("<script>alert('Please select an ecclesia.');</script>");
                return;
            }
            else
            {
                // Assign selected value if valid
                churchID = Convert.ToInt32(ecclesia.SelectedIndex);
            }

            // Validate password criteria (at least 8 characters, 1 uppercase, 1 number, 1 special character)
            if (!IsValidPassword(password))
            {
                Response.Write("<script>alert('Password must be at least 8 characters long and contain at least one uppercase letter, one number, and one special character.');</script>");
                return;
            }

            // If a profile picture is uploaded, use it. Otherwise, use the default profile picture.
            if (profilePictureUpload.HasFile)
            {
                // Get profile picture as byte array
                profilePicture = profilePictureUpload.FileBytes;
            }
            else
            {
                // Load default profile image from the server
                string defaultImagePath = Server.MapPath("~/Images/default_profile.png");
                profilePicture = File.ReadAllBytes(defaultImagePath);
            }

            // Instantiate the RegistrationManager class
            RegistrationManager registrationManager = new RegistrationManager();

            // Check if the email is already registered
            if (registrationManager.IsEmailRegistered(email))
            {
                Response.Write("<script>alert('This email is already registered. Please log in or use a different email.');</script>");
                return;
            }

            // Register the user through the RegistrationManager
            bool registrationSuccess = registrationManager.RegisterUser(firstName, surname, email, churchID, password, profilePicture);

            if (registrationSuccess)
            {
                // Registration succeeded, redirect to login or success page
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Registration failed, show an error message
                Response.Write("<script>alert('Registration failed. Please try again later.');</script>");
            }
        }

        // Define the LoginButton_Click method
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Redirect to login page when the login button is clicked
            Response.Redirect("Login.aspx");
        }

        // Helper method to validate email format using regular expressions
        private bool IsValidEmail(string email)
        {
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        // Helper method to validate password
        private bool IsValidPassword(string password)
        {
            // Password must be at least 8 characters, with at least one uppercase letter, one number, and one special character
            string passwordRegex = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, passwordRegex);
        }
    }
}
