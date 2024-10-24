using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class UserManager
    {

        // Get the connection string from Web.config
        private string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

        // Method to get the UserID based on session email
        public int GetUserIdByEmail(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UserID FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        object result = cmd.ExecuteScalar();
                        return (result != null) ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching UserID: " + ex.Message);
                return -1; // Return -1 in case of an error
            }
        }

        // Method to get the ChurchID based on UserID
        public int GetChurchIdByUserId(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ChurchID FROM Users WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        object result = cmd.ExecuteScalar();
                        return (result != null) ? Convert.ToInt32(result) : -1; // Return -1 if no ChurchID found
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching ChurchID: " + ex.Message);
                return -1; // Return -1 in case of an error
            }
        }



    }
}