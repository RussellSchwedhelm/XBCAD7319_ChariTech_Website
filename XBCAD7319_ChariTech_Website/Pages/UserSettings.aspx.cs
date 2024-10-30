using System;
using System.Data;
using XBCAD7319_ChariTech_Website.Classes;
using System.Web.UI.WebControls;


namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class UserSettings : System.Web.UI.Page
    {
        private ContactManager contactManager = new ContactManager(); // Assuming ContactManager fetches user data
        private UserManager userManager = new UserManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadUserPreferences();
                LoadUserProfilePicture();
                LoadUserAccountDetails(); // Load account details on page load

                // Assuming churchID is fetched as part of user details, here is an example:
                int churchID = GetUserChurchID(); // This method should retrieve churchID for the logged-in user
                PopulateEcclesiaDropdown(churchID); // Pass churchID to the dropdown population method
            }
        }

        // Example method to retrieve churchID (replace with actual retrieval logic)
        private int GetUserChurchID()
        {
            // Example: Retrieve churchID for the user from session or database
            return Convert.ToInt32(Session["UserChurchID"]);
        }


        // Method to populate the ecclesia drop-down list
        private void PopulateEcclesiaDropdown(int churchID)
        {
            RegistrationManager registrationManager = new RegistrationManager();
            DataTable churches = registrationManager.GetSortedChurches();

            // Clear any existing items in the drop-down list
            ddlEcclesia.Items.Clear();

            // Add church items from the fetched list
            foreach (DataRow row in churches.Rows)
            {
                ddlEcclesia.Items.Add(new ListItem(row["ChurchName"].ToString(), row["ChurchID"].ToString()));
            }

            // Set the selected index based on churchID + 1
            if (churchID >= 0 && churchID < ddlEcclesia.Items.Count)
            {
                ddlEcclesia.SelectedIndex = churchID + 1;
            }
        }


        // Method to load profile picture from the database
        private void LoadUserProfilePicture()
        {
            // Get the email of the authenticated user
            string email = Session["UserEmail"].ToString();

            // Fetch user data by email
            DataTable userData = userManager.GetUserDataByEmail(email); // Assume this method exists

            if (userData.Rows.Count > 0)
            {
                DataRow row = userData.Rows[0];
                byte[] profilePicBytes = row["ProfilePicture"] as byte[];

                // Convert profile picture to Base64 string if it exists
                if (profilePicBytes != null)
                { 
                    string profilePicBase64 = "data:image/png;base64," + Convert.ToBase64String(profilePicBytes);
                    userProfilePic.Attributes["src"] = profilePicBase64;
                }
                else
                {
                    // Fallback if no profile picture is available
                    userProfilePic.Attributes["src"] = "~/Images/ProfilePic.png";
                }
            }
        }

        // Method to load user account details (name, surname)
        private void LoadUserAccountDetails()
        {
            string email = Session["UserEmail"].ToString();
            DataTable userData = userManager.GetUserDataByEmail(email);

            if (userData.Rows.Count > 0)
            {
                DataRow row = userData.Rows[0];
                txtName.Text = row["FirstName"].ToString();
                txtSurname.Text = row["Surname"].ToString();
                txtEmail.Text = row["Email"].ToString();
            }
        }

        // Existing method to load user preferences
        protected void LoadUserPreferences()
        {
            UserManager userManager = new UserManager();
            int userId = userManager.GetUserIdByEmail(Session["UserEmail"].ToString());
            UserPreferenceDAL userPreferenceDAL = new UserPreferenceDAL();
            UserPreference userPreference = userPreferenceDAL.GetUserPreferences(userId);

            if (userPreference != null)
            {
                chkDarkModeCustom.Checked = userPreference.ThemePreferenceDark;
                chkBibleBasicsCustom.Checked = userPreference.BibleBasicsNotifications;
                chkResponsibiltyUpdatesCustom.Checked = userPreference.ResponsibilityUpdates;
            }
        }

        // Existing handler for saving settings
        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            UserPreference userPreference = new UserPreference
            {
                UserID = 123,
                ThemePreferenceDark = chkDarkModeCustom.Checked,
                BibleBasicsNotifications = chkBibleBasicsCustom.Checked,
                ResponsibilityUpdates = chkResponsibiltyUpdatesCustom.Checked
            };

            UserPreferenceDAL userPreferenceDAL = new UserPreferenceDAL();
            userPreferenceDAL.UpdateUserPreferences(userPreference);
        }
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            string newName = txtName.Text.Trim();
            string newSurname = txtSurname.Text.Trim();

            userManager.UpdateUserDetails(email, newName, newSurname);
            // Optionally, display a message to the user indicating the changes were saved
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Changes saved successfully!');", true);
        }

        // Handler for logging out
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear the session and redirect to login page
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }








        /*
        // Method to load profile picture from the database
        private void LoadUserProfilePicture()
        {
            string email = Session["UserEmail"].ToString();
            DataTable userData = userManager.GetUserDataByEmail(email);

            if (userData.Rows.Count > 0)
            {
                DataRow row = userData.Rows[0];
                byte[] profilePicBytes = row["ProfilePicture"] as byte[];

                if (profilePicBytes != null)
                {
                    string profilePicBase64 = "data:image/png;base64," + Convert.ToBase64String(profilePicBytes);
                    userProfilePic.Attributes["src"] = profilePicBase64;
                }
                else
                {
                    userProfilePic.Attributes["src"] = "~/Images/default_profile.png"; // Use default image if no picture
                }
            }
        }*/

        protected void btnUploadPicture_Click(object sender, EventArgs e)
        {
            if (profilePictureUpload.HasFile)
            {
                try
                {
                    byte[] profilePictureBytes = profilePictureUpload.FileBytes;
                    string email = Session["UserEmail"].ToString();
                    bool updateSuccess = userManager.UpdateUserProfilePicture(email, profilePictureBytes);

                    if (updateSuccess)
                    {
                        // Update the displayed image
                        userProfilePic.Attributes["src"] = "data:image/png;base64," + Convert.ToBase64String(profilePictureBytes);
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Profile picture updated successfully!');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update profile picture.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error uploading profile picture: " + ex.Message + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a profile picture to upload.');", true);
            }
        }

        /*
        protected void btnUploadPicture_Click(object sender, EventArgs e)
        {
            if (profilePictureUpload.HasFile)
            {
                try
                {
                    // Read the uploaded file into a byte array
                    byte[] profilePictureBytes = profilePictureUpload.FileBytes;

                    // Get the email of the authenticated user
                    string email = Session["UserEmail"].ToString();

                    // Update the profile picture in the database
                    bool updateSuccess = userManager.UpdateUserProfilePicture(email, profilePictureBytes);

                    if (updateSuccess)
                    {
                        // Update the displayed image
                        userProfilePic.Attributes["src"] = "data:image/png;base64," + Convert.ToBase64String(profilePictureBytes);
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Profile picture updated successfully!');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update profile picture.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error uploading profile picture: " + ex.Message + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a profile picture to upload.');", true);
            }
        }*/

    }
}
