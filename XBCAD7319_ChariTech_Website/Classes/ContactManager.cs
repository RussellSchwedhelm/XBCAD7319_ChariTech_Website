﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class ContactManager
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

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

    }
}
