using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;  // Add this for SQL Server interactions
using System.Configuration;  // Add this for ConfigurationManager

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class UserPreferenceDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AzureDBConnectionString"].ConnectionString;

        // Get user preferences by UserID
        public UserPreference GetUserPreferences(int userID)
        {
            UserPreference userPreference = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.UserPreference WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    userPreference = new UserPreference
                    {
                        UserPreferenceID = (int)reader["UserPreferenceID"],
                        UserID = (int)reader["UserID"],
                        ThemePreferenceDark = (bool)reader["ThemePreferenceDark"],
                        Volume = (int)reader["Volume"],
                        ButtonClicksSound = (bool)reader["ButtonClicksSound"],
                        BibleBasicsNotifications = (bool)reader["BibleBasicsNotifications"],
                        ResponsibilityUpdates = (bool)reader["ResponsibilityUpdates"]
                    };
                }

                reader.Close();
            }

            return userPreference;
        }

        // Update user preferences
        public void UpdateUserPreferences(UserPreference userPreference)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE dbo.UserPreference " +
                               "SET ThemePreferenceDark = @ThemePreferenceDark, Volume = @Volume, " +
                               "ButtonClicksSound = @ButtonClicksSound, BibleBasicsNotifications = @BibleBasicsNotifications, " +
                               "ResponsibilityUpdates = @ResponsibilityUpdates " +
                               "WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userPreference.UserID);
                cmd.Parameters.AddWithValue("@ThemePreferenceDark", userPreference.ThemePreferenceDark);
                cmd.Parameters.AddWithValue("@Volume", userPreference.Volume);
                cmd.Parameters.AddWithValue("@ButtonClicksSound", userPreference.ButtonClicksSound);
                cmd.Parameters.AddWithValue("@BibleBasicsNotifications", userPreference.BibleBasicsNotifications);
                cmd.Parameters.AddWithValue("@ResponsibilityUpdates", userPreference.ResponsibilityUpdates);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
