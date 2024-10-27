using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class UserSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load user preferences when the page first loads
                LoadUserPreferences();
            }
        }



        // Event handler for the Save button
        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            // Collect user preferences from form inputs
            UserPreference userPreference = new UserPreference
            {
                UserID = 123, // Replace this with the actual user ID logic, e.g., from session
                ThemePreferenceDark = chkDarkModeCustom.Checked,
                BibleBasicsNotifications = chkBibleBasicsCustom.Checked,
                ResponsibilityUpdates = chkResponsibiltyUpdatesCustom.Checked
            };

            // Save user preferences to the database
            UserPreferenceDAL userPreferenceDAL = new UserPreferenceDAL();
            userPreferenceDAL.UpdateUserPreferences(userPreference);
        }

        // Method to load user preferences from the database
        protected void LoadUserPreferences()
        {
            int userId = 123; // Replace this with actual user ID logic, e.g., from session
            UserPreferenceDAL userPreferenceDAL = new UserPreferenceDAL();
            UserPreference userPreference = userPreferenceDAL.GetUserPreferences(userId);

            // Initialize form controls with loaded user preferences
            if (userPreference != null)
            {
                chkDarkModeCustom.Checked = userPreference.ThemePreferenceDark;
                chkBibleBasicsCustom.Checked = userPreference.BibleBasicsNotifications;
                chkResponsibiltyUpdatesCustom.Checked = userPreference.ResponsibilityUpdates;
            }
        }
    }
}