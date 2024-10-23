using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class NewsletterManager
    {
        // Get the connection string from Web.config
        private string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

        // Method to get the UserID based on session email
        private int GetUserIdByEmail(string email)
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

        // Method to upload a newsletter
        public bool UploadNewsletter(string email, int churchId, string title, DateTime issueDate, HttpPostedFile uploadedFile)
        {
            byte[] pdfBytes = null;

            // Check if the uploaded file is not null and is a PDF
            if (uploadedFile != null && uploadedFile.ContentType == "application/pdf")
            {
                using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                {
                    pdfBytes = binaryReader.ReadBytes(uploadedFile.ContentLength);
                }
            }
            else
            {
                return false; // File is not a PDF, return false
            }

            // Get the user ID from the email stored in the session
            int uploadedUserId = GetUserIdByEmail(email);
            if (uploadedUserId == -1)
            {
                // User ID was not found, return false
                return false;
            }

            // Insert the newsletter into the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO dbo.Newsletter (UploadedUserID, ChurchID, Title, IssueDate, PdfContent)
                    VALUES (@UploadedUserID, @ChurchID, @Title, @IssueDate, @PdfContent)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UploadedUserID", uploadedUserId);
                    command.Parameters.AddWithValue("@ChurchID", churchId);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@IssueDate", issueDate);
                    command.Parameters.AddWithValue("@PdfContent", (object)pdfBytes ?? DBNull.Value);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Return true if the insert was successful
                }
            }
        }
        // Method to retrieve a list of newsletters
        public DataTable GetNewsletters()
        {
            DataTable newslettersTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT NewsletterID, Title, IssueDate FROM dbo.Newsletter";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(newslettersTable);
                }
            }

            return newslettersTable;
        }

        // Method to retrieve the PDF content by NewsletterID
        public byte[] GetNewsletterPdf(int newsletterId)
        {
            byte[] pdfBytes = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PdfContent FROM dbo.Newsletter WHERE NewsletterID = @NewsletterID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewsletterID", newsletterId);
                    connection.Open();

                    pdfBytes = command.ExecuteScalar() as byte[];
                }
            }

            return pdfBytes;
        }
    }
}