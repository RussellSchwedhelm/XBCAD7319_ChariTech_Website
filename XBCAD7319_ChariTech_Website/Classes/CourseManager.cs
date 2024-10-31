using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class CourseManager
    {
        // Get the connection string from Web.config
        private string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
        //---------------------------------------------------------------------------------------------------------------------//
        public bool SaveCourse(CourseClass course)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO dbo.Courses (CourseTitle, Theme, Duration, DateUploaded, Description, PdfFileContent, ChurchID)
            VALUES (@CourseTitle, @Theme, @Duration, @DateUploaded, @Description, @PdfFileContent, @ChurchID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseTitle", course.CourseTitle);
                    command.Parameters.AddWithValue("@Theme", course.Theme);
                    command.Parameters.AddWithValue("@Duration", course.Duration);
                    command.Parameters.AddWithValue("@DateUploaded", DateTime.Now);
                    command.Parameters.AddWithValue("@Description", course.Description);
                    command.Parameters.AddWithValue("@PdfFileContent", (object)course.PdfFileContent ?? DBNull.Value); // Save PDF bytes
                    command.Parameters.AddWithValue("@ChurchID", course.ChurchID);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Return true if the insert was successful
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
        // Method to retrieve a list of courses from the database
        public List<CourseClass> GetAllCourses(int parsedID)
        {
            List<CourseClass> courses = new List<CourseClass>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CourseTitle, Theme, Duration, DateUploaded, Description, PdfFileContent, ChurchID FROM Courses WHERE ChurchID = @ChurchID"; 



                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ChurchID", parsedID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new CourseClass object and populate it with data from the database
                            CourseClass course = new CourseClass
                            {
                                CourseTitle = reader["CourseTitle"].ToString(),
                                Theme = reader["Theme"].ToString(),
                                Duration = reader["Duration"].ToString(),
                                DateUploaded = reader["DateUploaded"].ToString(),
                                Description = reader["Description"].ToString(),
                                PdfFileContent = reader["PdfFileContent"] as byte[], // Cast to byte[] for PdfFileContent
                                ChurchID = Convert.ToInt32(reader["ChurchID"])
                            };

                            // Add the course to the list
                            courses.Add(course);
                        }
                    }
                }
            }

            return courses; // Return the list of courses
        }
        //---------------------------------------------------------------------------------------------------------------------//
        public string SavePdfFromByteArray(byte[] pdfContent, string fileName)
        {
            if (pdfContent == null || pdfContent.Length == 0)
                return null;

            // Define the directory path for saving PDFs
            string folderPath = HttpContext.Current.Server.MapPath("~/Content/CoursePdfs/");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Construct the full file path
            string filePath = Path.Combine(folderPath, fileName + ".pdf");

            // Save the byte array as a PDF file
            File.WriteAllBytes(filePath, pdfContent);

            // Return the relative URL to the PDF file for client access
            return $"~/Content/CoursePdfs/{fileName}.pdf";
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//