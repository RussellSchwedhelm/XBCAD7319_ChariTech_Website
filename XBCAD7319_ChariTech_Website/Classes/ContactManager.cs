using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class ContactManager
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

       
        // Method to fetch ChurchID of the current logged-in user by their email
        public int GetChurchIDByEmail(string email)
        {
            int churchID = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ChurchID FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    churchID = (int)cmd.ExecuteScalar();
                }
            }
            return churchID;
        }

        // Method to fetch users with RoleID = 2 and the same ChurchID as the current user
        public DataTable GetUsersByChurchID(int churchID)
        {
            DataTable usersTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT FirstName, Surname, Email, ContactNumber, ProfilePicture FROM Users WHERE ChurchID = @ChurchID AND RoleID = 2";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChurchID", churchID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(usersTable);
                }
            }
            return usersTable;
        }

    }
}
