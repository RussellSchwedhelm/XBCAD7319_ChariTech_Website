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
                // Only retrieve the essential fields (Title, Date, Speaker), not the audio file
                string query = "SELECT ExhortationID, Title, Speaker, Date FROM Exhortation WHERE ChurchID = @ChurchID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChurchID", churchID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable exhortations = new DataTable();
                    da.Fill(exhortations);  // Fill the DataTable with exhortation metadata
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

        public byte[] GetExhortationAudio(int exhortationId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            byte[] audioBytes = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT AudioFile FROM Exhortation WHERE ExhortationID = @ExhortationID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);
                        conn.Open();
                        audioBytes = cmd.ExecuteScalar() as byte[];  // Retrieve the MP3 binary data
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the error if needed
                System.Diagnostics.Debug.WriteLine("SQL error occurred: " + ex.Message);

                // Reload the page in case of an SQL error
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                // Log the error if needed
                System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);

                // Reload the page in case of any other error
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl, true);
            }

            return audioBytes;
        }



        public Exhortation GetExhortationById(int exhortationId)
        {
            Exhortation exhortation = null;
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Exhortation WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            exhortation = new Exhortation
                            {
                                ExhortationID = (int)reader["ExhortationID"],
                                UploadedUserID = (int)reader["UploadedUserID"],
                                ChurchID = (int)reader["ChurchID"],
                                AITranscriptionID = reader["AITranscriptionID"] as int?,
                                AISummaryID = reader["AISummaryID"] as int?,
                                Title = reader["Title"].ToString(),
                                Date = (DateTime)reader["Date"],
                                Speaker = reader["Speaker"].ToString(),
                                TranscriptionPrompt = reader["TranscriptionPrompt"] as string,
                                SummaryPrompt = reader["SummaryPrompt"] as string,
                                AudioFile = reader["AudioFile"] as byte[]
                            };
                        }
                    }
                }
            }

            return exhortation;
        }


        public AITranscription GetAITranscriptionById(int transcriptionId)
        {
            AITranscription transcription = null;
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM AITranscription WHERE AITranscriptionID = @AITranscriptionID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AITranscriptionID", transcriptionId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            transcription = new AITranscription
                            {
                                AITranscriptionID = (int)reader["AITranscriptionID"],
                                ExhortationID = (int)reader["ExhortationID"],
                                TranscriptionText = reader["TranscriptionText"] as string,
                                AIProcessingStatus = reader["AIProcessingStatus"].ToString(),
                                StartedAt = (DateTime)reader["StartedAt"],
                                EndedAt = reader["EndedAt"] as DateTime?,
                                TranscriptionProcessingTime = reader["TranscriptionProcessingTime"] as int?,
                                IsTranscriptionEdited = (bool)reader["IsTranscriptionEdited"]
                            };
                        }
                    }
                }
            }

            return transcription;
        }


        public AISummary GetAISummaryById(int summaryId)
        {
            AISummary summary = null;
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM AISummary WHERE AISummaryID = @AISummaryID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AISummaryID", summaryId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary = new AISummary
                            {
                                AISummaryID = (int)reader["AISummaryID"],
                                ExhortationID = (int)reader["ExhortationID"],
                                SummaryText = reader["SummaryText"] as string,
                                AIProcessingStatus = reader["AIProcessingStatus"].ToString(),
                                StartedAt = (DateTime)reader["StartedAt"],
                                EndedAt = reader["EndedAt"] as DateTime?,
                                SummaryProcessingTime = reader["SummaryProcessingTime"] as int?,
                                IsSummaryEdited = (bool)reader["IsSummaryEdited"]
                            };
                        }
                    }
                }
            }

            return summary;
        }




    }
}
