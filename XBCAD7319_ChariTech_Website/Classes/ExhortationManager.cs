using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class ExhortationManager
    {
        // Method to retrieve ChurchID by Email from the Users table
        public int GetChurchIdByEmail(string email)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            int churchId = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ChurchID FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        churchId = Convert.ToInt32(result);
                    }
                }
            }

            return churchId;
        }

        // Method to retrieve exhortations by ChurchID
        public DataTable GetExhortationsByChurchID(int churchID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ExhortationID, Title, AudioFilePath FROM Exhortation WHERE ChurchID = @ChurchID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChurchID", churchID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable exhortations = new DataTable();
                    da.Fill(exhortations);  // Fill the DataTable with exhortations data
                    return exhortations;
                }
            }
        }

        // Method to upload exhortation
        public bool UploadExhortation(string email, int churchId, string title, string speaker, DateTime issueDate, HttpPostedFile uploadedFile)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Exhortation (UploadedUserID, ChurchID, Title, Speaker, Date, AudioFile) 
                    VALUES ((SELECT UserID FROM Users WHERE Email = @Email), @ChurchID, @Title, @Speaker, @Date, @AudioFile)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Get the MP3 file as binary data
                    byte[] audioFileData;
                    using (BinaryReader br = new BinaryReader(uploadedFile.InputStream))
                    {
                        audioFileData = br.ReadBytes(uploadedFile.ContentLength);
                    }

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@ChurchID", churchId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Speaker", speaker);
                    cmd.Parameters.AddWithValue("@Date", issueDate);
                    cmd.Parameters.AddWithValue("@AudioFile", audioFileData); // Store binary data

                    // Open connection and execute query
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
    }
}
