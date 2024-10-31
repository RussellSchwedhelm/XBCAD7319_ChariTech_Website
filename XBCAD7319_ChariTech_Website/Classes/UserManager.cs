using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class UserManager
    {
        // Get the connection string from Web.config
        private string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

        //---------------------------------------------------------------------------------------------------------------------//
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
        //---------------------------------------------------------------------------------------------------------------------//
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
        //---------------------------------------------------------------------------------------------------------------------//

        public string GetFirstNameByEmail(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT FirstName FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        object result = cmd.ExecuteScalar();
                        return result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching first name: " + ex.Message);
                return "";
            }
        }

        //---------------------------------------------------------------------------------------------------------------------//
        public void UpdateUserDetails(string email, string newName, string newSurname)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET FirstName = @FirstName, Surname = @Surname WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", newName);
                    cmd.Parameters.AddWithValue("@Surname", newSurname);
                    cmd.Parameters.AddWithValue("@Email", email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------------------//
        public DataTable GetUserDataByEmail(string email)
        {
            DataTable dt = new DataTable();
            string query = "SELECT FirstName, Surname, Email, ProfilePicture FROM Users WHERE Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        //---------------------------------------------------------------------------------------------------------------------//
        public bool UpdateUserProfilePicture(string email, byte[] profilePicture)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Users SET ProfilePicture = @ProfilePicture WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicture", (object)profilePicture ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", email);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        public byte[] GetProfilePictureByEmail(string email)
        {
            byte[] profilePicture = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ProfilePicture FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    profilePicture = cmd.ExecuteScalar() as byte[];
                }
            }
            return profilePicture;
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//