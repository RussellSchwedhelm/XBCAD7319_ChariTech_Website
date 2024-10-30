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
        // Retrieve the database connection string from the Web.config file
        private string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

        // Method to get the unique UserID for a given email address
        public int GetUserIdByEmail(string email)
        {
            try
            {
                // Create a new SQL connection using the connection string
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Open the SQL connection

                    // SQL query to select the UserID based on the provided email
                    string query = "SELECT UserID FROM Users WHERE Email = @Email";

                    // Initialize a SQL command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the email parameter to the SQL command
                        cmd.Parameters.AddWithValue("@Email", email);

                        // Execute the query and retrieve a single result (UserID)
                        object result = cmd.ExecuteScalar();

                        // If a result is found, return it as an integer, otherwise return -1
                        return (result != null) ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching UserID: " + ex.Message);
                return -1; // Return -1 if an error occurs
            }
        }

        // Method to handle the upload of a newsletter PDF file
        public bool UploadNewsletter(string email, int churchId, string title, DateTime issueDate, HttpPostedFile uploadedFile)
        {
            byte[] pdfBytes = null; // Initialize variable to hold PDF file bytes

            // Check if a file is provided and if it is a PDF file
            if (uploadedFile != null && uploadedFile.ContentType == "application/pdf")
            {
                // Read the uploaded file into a byte array
                using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                {
                    pdfBytes = binaryReader.ReadBytes(uploadedFile.ContentLength);
                }
            }
            else
            {
                return false; // Return false if the file is not a PDF
            }

            // Retrieve the user ID associated with the email
            int uploadedUserId = GetUserIdByEmail(email);
            if (uploadedUserId == -1)
            {
                // If no valid user ID is found, return false
                return false;
            }

            // SQL insertion of the newsletter into the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to insert newsletter details and PDF content into the database
                string query = @"
                    INSERT INTO dbo.Newsletter (UploadedUserID, ChurchID, Title, IssueDate, PdfContent)
                    VALUES (@UploadedUserID, @ChurchID, @Title, @IssueDate, @PdfContent)";

                // Create a SQL command for the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL command
                    command.Parameters.AddWithValue("@UploadedUserID", uploadedUserId);
                    command.Parameters.AddWithValue("@ChurchID", churchId);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@IssueDate", issueDate);
                    command.Parameters.AddWithValue("@PdfContent", (object)pdfBytes ?? DBNull.Value);

                    connection.Open(); // Open the SQL connection

                    // Execute the command, returns the number of affected rows
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Return true if the insertion was successful
                }
            }
        }

        // Method to retrieve a list of newsletters, returning a DataTable
        public DataTable GetNewsletters(int churchID)
        {
            DataTable newslettersTable = new DataTable(); // Initialize DataTable to hold newsletter data

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to retrieve the newsletter ID, title, and issue date
                string query = "SELECT NewsletterID, Title, IssueDate FROM dbo.Newsletter WHERE ChurchID = @ChurchID";

                // Create a SQL command for the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ChurchID", SqlDbType.Int).Value = churchID; // Specify the type for the parameter
                    connection.Open(); // Open the SQL connection

                    // Adapter to fill the DataTable with the result of the SQL command
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            adapter.Fill(newslettersTable); // Populate the DataTable
                        }
                        catch (SqlException ex)
                        {
                            // Handle SQL exceptions here (e.g., logging)
                            throw new Exception("Error retrieving newsletters from the database.", ex);
                        }
                    }
                }
            }
            return newslettersTable; // Return the populated DataTable
        }

        // Method to retrieve the PDF content for a specific newsletter by ID
        public byte[] GetNewsletterPdf(int newsletterId)
        {
            byte[] pdfBytes = null; // Initialize variable to hold PDF bytes

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to retrieve the PDF content based on the newsletter ID
                string query = "SELECT PdfContent FROM dbo.Newsletter WHERE NewsletterID = @NewsletterID";

                // Create a SQL command for the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add newsletter ID parameter to the SQL command
                    command.Parameters.AddWithValue("@NewsletterID", newsletterId);
                    connection.Open(); // Open the SQL connection

                    // Execute the query and retrieve the PDF content as a byte array
                    pdfBytes = command.ExecuteScalar() as byte[];
                }
            }

            return pdfBytes; // Return the byte array containing the PDF content
        }

        // Method to search for newsletters by church ID and search term in the title
        public DataTable SearchNewsletters(int churchId, string searchTerm)
        {
            DataTable newslettersTable = new DataTable(); // Initialize DataTable to hold search results

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to retrieve newsletters matching the church ID and title search term
                string query = @"
                    SELECT NewsletterID, Title, IssueDate 
                    FROM dbo.Newsletter 
                    WHERE ChurchID = @ChurchID AND Title LIKE @SearchTerm
                    ORDER BY Title";

                // Create a SQL command for the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for the church ID and search term
                    command.Parameters.AddWithValue("@ChurchID", churchId);
                    command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    connection.Open(); // Open the SQL connection

                    // Adapter to fill the DataTable with the search results
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(newslettersTable); // Populate the DataTable
                }
            }

            return newslettersTable; // Return the populated DataTable with search results
        }
    }
}
