using System;
using System.Data;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class UserSettings : System.Web.UI.Page
    {
        private ContactManager contactManager = new ContactManager(); // Assuming ContactManager fetches user data

        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to login if user is not authenticated
          /*  if (Session["UserEmail"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                // Load user preferences when the page first loads
                LoadUserPreferences();

                // Load profile picture
                LoadUserProfilePicture();
            }*/
        }

        // Method to load profile picture from the database
        private void LoadUserProfilePicture()
        {
            // Get the email of the authenticated user
            string email = Session["UserEmail"].ToString();

            // Fetch user data by email
            DataTable userData = contactManager.GetUserDataByEmail(email); // Assume this method exists

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

        // Existing method to load user preferences
        protected void LoadUserPreferences()
        {
            int userId = 123; // Replace this with actual user ID logic, e.g., from session
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

    }
}
