using System;
using System.Data.SqlClient;
using System.Web;

public partial class DownloadExhortation : System.Web.UI.Page
{
    //---------------------------------------------------------------------------------------------------------------------//
    protected void Page_Load(object sender, EventArgs e)
    {
        int exhortationId;
        if (int.TryParse(Request.QueryString["id"], out exhortationId))
        {
            byte[] audioBytes = GetExhortationAudio(exhortationId);
            if (audioBytes != null)
            {
                try
                {
                    Response.Clear();
                    Response.ContentType = "audio/mpeg"; // Set MIME type to audio
                    Response.AddHeader("Content-Disposition", "inline; filename=exhortation_" + exhortationId + ".mp3");
                    Response.OutputStream.Write(audioBytes, 0, audioBytes.Length);
                    Response.Flush();
                }
                catch (HttpException ex)
                {
                    // Log HTTP errors (if logging is available)
                    System.Diagnostics.Debug.WriteLine($"HTTP error occurred: {ex.Message}");
                    Response.StatusCode = 500;
                }
                finally
                {
                    Response.End();
                }
            }
            else
            {
                // Log the missing file issue
                System.Diagnostics.Debug.WriteLine($"Audio file for ExhortationID {exhortationId} not found.");
                Response.StatusCode = 404;
                Response.StatusDescription = "Audio file not found.";
                Response.End();
            }
        }
        else
        {
            // Log invalid query parameter
            System.Diagnostics.Debug.WriteLine("Invalid ExhortationID query parameter.");
            Response.StatusCode = 400;
            Response.StatusDescription = "Invalid ExhortationID parameter.";
            Response.End();
        }
    }
    //---------------------------------------------------------------------------------------------------------------------//

    private byte[] GetExhortationAudio(int exhortationId)
    {
        // Retrieve the audio file binary from the database based on exhortationId
        string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
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
                    audioBytes = cmd.ExecuteScalar() as byte[];

                    // Check if audioBytes was retrieved and log if null
                    if (audioBytes == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"No audio found for ExhortationID {exhortationId}.");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // Log SQL errors for diagnostics
            System.Diagnostics.Debug.WriteLine($"SQL error occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Log any other types of errors
            System.Diagnostics.Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return audioBytes;
    }
    //---------------------------------------------------------------------------------------------------------------------//
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//