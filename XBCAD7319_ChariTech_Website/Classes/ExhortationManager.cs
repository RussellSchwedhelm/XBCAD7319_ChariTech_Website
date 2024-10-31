// Required namespaces for Azure Speech and SQL functionalities
using Microsoft.CognitiveServices.Speech;       // Provides access to Azure Speech services
using Microsoft.CognitiveServices.Speech.Audio; // Handles audio input for speech recognition
using NAudio.Wave;
using Newtonsoft.Json;
using System;                                   // Provides basic system functions
using System.Configuration;                     // Allows access to configuration settings
using System.Data;                              // Supports ADO.NET and data handling
using System.Data.SqlClient;                    // Provides SQL Server data provider
using System.IO;                                // Provides I/O capabilities for file handling
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;                   // Enables asynchronous programming
using System.Web;                               // Supports web applications
using System.Web.Configuration;                 // Allows access to web configuration


namespace XBCAD7319_ChariTech_Website.Classes
{
    public class ExhortationManager
    {
        // Method to retrieve ChurchID using Email from the Users table
        public int GetChurchIdByEmail(string email)
        {
            // Retrieve the connection string from Web.config
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            int churchId = -1; // Default ID in case no match is found

            // Using block for SQL connection to ensure disposal after use
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // SQL query to select ChurchID where email matches
                string query = "SELECT ChurchID FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email); // Add parameter to prevent SQL injection

                    conn.Open(); // Open SQL connection
                    object result = cmd.ExecuteScalar(); // Execute query, expecting a single result

                    if (result != null)
                    {
                        churchId = Convert.ToInt32(result); // Convert result to int if not null
                    }
                }
            }

            return churchId; // Return retrieved ChurchID or -1 if not found
        }

        // Method to retrieve exhortations for a given ChurchID
        public DataTable GetExhortationsByChurchID(int churchID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        ExhortationID, 
                        Title, 
                        Speaker, 
                        Date, 
                        AISummaryText,
                        AITranscriptionText
                    FROM 
                        Exhortation
                    WHERE 
                        ChurchID = @ChurchID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChurchID", churchID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable exhortations = new DataTable();
                    da.Fill(exhortations);
                    return exhortations;
                }
            }
        }

        // Method to search exhortations by church ID and search term with prioritization
        public DataTable SearchExhortations(int churchID, string searchTerm)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Modified query to prioritize Title matches first, then Summary matches
                string query = @"
            SELECT 
                ExhortationID, 
                Title, 
                Speaker, 
                Date, 
                AISummaryText,
                AITranscriptionText,
                CASE 
                    WHEN Title LIKE @SearchTerm THEN 1 
                    WHEN AISummaryText LIKE @SearchTerm THEN 2 
                    ELSE 3 
                END AS SortOrder
            FROM 
                Exhortation
            WHERE 
                ChurchID = @ChurchID 
                AND 
                (Title LIKE @SearchTerm OR AISummaryText LIKE @SearchTerm OR AITranscriptionText LIKE @SearchTerm)
            ORDER BY 
                SortOrder, Title";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChurchID", churchID);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable searchResults = new DataTable();
                    da.Fill(searchResults);
                    return searchResults;
                }
            }
        }

        // Method to upload an exhortation with details and audio file
        public bool UploadExhortation(string email, int churchId, string title, string speaker, DateTime issueDate, HttpPostedFile uploadedFile)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            int exhortationId = -1; // Default ID if upload fails
            byte[] audioFileData;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // SQL query to insert new exhortation data and get the generated ExhortationID
                string query = @"
            INSERT INTO Exhortation (UploadedUserID, ChurchID, Title, Speaker, Date, AudioFile) 
            OUTPUT INSERTED.ExhortationID
            VALUES ((SELECT UserID FROM Users WHERE Email = @Email), @ChurchID, @Title, @Speaker, @Date, @AudioFile)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (BinaryReader br = new BinaryReader(uploadedFile.InputStream))
                    {
                        audioFileData = br.ReadBytes(uploadedFile.ContentLength); // Convert uploaded audio file to byte array
                    }

                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@ChurchID", churchId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Speaker", speaker);
                    cmd.Parameters.AddWithValue("@Date", issueDate);
                    cmd.Parameters.AddWithValue("@AudioFile", audioFileData);

                    conn.Open();
                    exhortationId = (int)cmd.ExecuteScalar(); // Execute and retrieve inserted ExhortationID
                }
            }

            if (exhortationId > 0)
            {
                // Start transcription process asynchronously
                Task.Run(async () => await StartTranscriptionAsync(exhortationId, audioFileData));
            }

            return exhortationId > 0;
        }

        private async Task StartTranscriptionAsync(int exhortationId, byte[] mp3FileData)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Starting transcription for ExhortationID: " + exhortationId);
                var speechServiceKey = ConfigurationManager.AppSettings["SpeechServiceKey"];
                var speechServiceRegion = ConfigurationManager.AppSettings["SpeechServiceRegion"];
                var config = SpeechConfig.FromSubscription(speechServiceKey, speechServiceRegion);

                using (var mp3Stream = new MemoryStream(mp3FileData))
                using (var mp3Reader = new Mp3FileReader(mp3Stream))
                using (var resampledStream = new WaveFormatConversionStream(new WaveFormat(16000, 16, 1), mp3Reader))
                using (var wavStream = new MemoryStream())
                {
                    WaveFileWriter.WriteWavFileToStream(wavStream, resampledStream);
                    wavStream.Position = 0;

                    using (var audioConfig = AudioConfig.FromStreamInput(AudioInputStream.CreatePullStream(new BinaryAudioStreamReader(wavStream))))
                    using (var recognizer = new SpeechRecognizer(config, audioConfig))
                    {
                        string fullTranscriptionText = string.Empty;
                        var stopRecognition = new TaskCompletionSource<int>();

                        recognizer.Recognized += (s, e) =>
                        {
                            if (e.Result.Reason == ResultReason.RecognizedSpeech)
                            {
                                fullTranscriptionText += e.Result.Text + " ";
                            }
                        };

                        recognizer.SessionStopped += (s, e) => stopRecognition.TrySetResult(0);
                        recognizer.Canceled += (s, e) => stopRecognition.TrySetResult(0);

                        await recognizer.StartContinuousRecognitionAsync();
                        await stopRecognition.Task;
                        await recognizer.StopContinuousRecognitionAsync();

                        if (!string.IsNullOrEmpty(fullTranscriptionText))
                        {
                            UpdateExhortationTranscription(exhortationId, fullTranscriptionText.Trim());

                            // Generate summary based on transcription text
                            string summaryText = await GenerateSummaryWithRetryAsync(fullTranscriptionText.Trim(), GetSpeakerNameByExhortationId(exhortationId));
                            if (!string.IsNullOrEmpty(summaryText))
                            {
                                UpdateExhortationSummary(exhortationId, summaryText);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in StartTranscriptionAsync: " + ex.Message);
            }
        }

        private async Task<string> GenerateSummaryWithRetryAsync(string transcriptionText, string speakerName)
        {
            string summaryPrompt = $"Based on the following transcription by {speakerName}, create a summary of no more than 2 paragraphs. The summary should capture what the exhortation was all about: {transcriptionText}";
            int maxRetries = 5;
            int delay = 1000; // initial delay of 1 second

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    var apiKey = ConfigurationManager.AppSettings["OpenAIApiKey"];
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    var requestBody = new
                    {
                        model = "gpt-3.5-turbo", // Change to the preferred model
                        messages = new[]
                        {
                    new {
                        role = "user",
                        content = new[] { new { type = "text", text = summaryPrompt } }
                    }
                }
                    };

                    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions",
                        new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        dynamic result = JsonConvert.DeserializeObject(responseBody);

                        // Extract the generated message content
                        string generatedMessage = result.choices[0].message.content.ToString();

                        // Print the generated message to the debug console
                        System.Diagnostics.Debug.WriteLine("Generated Summary: " + generatedMessage);

                        // Return the generated message
                        return generatedMessage;
                    }
                    else if ((int)response.StatusCode == 429)
                    {
                        // Log and delay before retry
                        System.Diagnostics.Debug.WriteLine("Rate limit hit. Retrying in " + delay + "ms.");
                        await Task.Delay(delay);
                        delay *= 2; // exponential backoff
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Request failed with status code: " + response.StatusCode);
                        System.Diagnostics.Debug.WriteLine("Reason: " + response.ReasonPhrase);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error in GenerateSummaryAsync: " + ex.Message);
                    break;
                }
            }
            return null;
        }

        public void UpdateExhortationSummary(int exhortationId, string summaryText)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Exhortation SET AISummaryText = @SummaryText WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SummaryText", summaryText);
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Custom class to enable reading from a MemoryStream
        public class BinaryAudioStreamReader : PullAudioInputStreamCallback
        {
            private readonly Stream _stream;

            public BinaryAudioStreamReader(Stream stream)
            {
                _stream = stream;
            }

            public override int Read(byte[] buffer, uint size)
            {
                return _stream.Read(buffer, 0, (int)size);
            }

            protected override void Dispose(bool disposing)
            {
                _stream.Dispose();
                base.Dispose(disposing);
            }
        }

        // Method to update transcription text in the database for a specific exhortation
        public void UpdateExhortationTranscription(int exhortationId, string transcriptionText)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Exhortation SET AITranscriptionText = @TranscriptionText WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TranscriptionText", transcriptionText);
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to retrieve audio data for a given ExhortationID
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
                        audioBytes = cmd.ExecuteScalar() as byte[]; // Retrieve audio data as byte array
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL error occurred: " + ex.Message);
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl, true);
            }

            return audioBytes;
        }

        // Method to retrieve an exhortation by ExhortationID
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
                                Title = reader["Title"].ToString(),
                                Date = (DateTime)reader["Date"],
                                Speaker = reader["Speaker"].ToString(),
                                AudioFile = reader["AudioFile"] as byte[],
                                AISummaryText = reader["AISummaryText"] as string,
                                AITranscriptionText = reader["AITranscriptionText"] as string
                            };
                        }
                    }
                }
            }

            return exhortation;
        }

        // Method to retrieve AI transcription details by AITranscriptionID
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
                            // Populate AITranscription object with data
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

            return transcription; // Return populated AITranscription object
        }

        // Method to retrieve AI summary details by AISummaryID
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
                            // Populate AISummary object with data
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

            return summary; // Return populated AISummary object
        }
        public string GetSpeakerNameByExhortationId(int exhortationId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            string speakerName = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // SQL query to select the Speaker's name based on ExhortationID
                string query = "SELECT Speaker FROM Exhortation WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);

                    conn.Open(); // Open the SQL connection
                    object result = cmd.ExecuteScalar(); // Execute query and retrieve the speaker name

                    if (result != null && result != DBNull.Value)
                    {
                        speakerName = result.ToString(); // Convert result to string if not null
                    }
                }
            }

            return speakerName; // Return the speaker's name or null if not found
        }
        // Method to delete an exhortation by ExhortationID
        public bool DeleteExhortationById(int exhortationId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            bool isDeleted = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Exhortation WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isDeleted = rowsAffected > 0; // Returns true if at least one row was deleted
                }
            }

            return isDeleted;
        }

        // Method to update the transcript of an exhortation by ExhortationID
        public bool UpdateExhortationTranscript(int exhortationId, string transcriptionText)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Exhortation SET AITranscriptionText = @TranscriptionText WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TranscriptionText", transcriptionText);
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0; // Returns true if the update was successful
                }
            }

            return isUpdated;
        }

        public void UpdateExhortationDetails(int exhortationId, string title, string speaker, DateTime date, string transcriptionText, string summaryText)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Exhortation 
                         SET Title = @Title, Speaker = @Speaker, Date = @Date, 
                             AITranscriptionText = @TranscriptionText, AISummaryText = @SummaryText 
                         WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Speaker", speaker);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@TranscriptionText", transcriptionText);
                    cmd.Parameters.AddWithValue("@SummaryText", summaryText);
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}