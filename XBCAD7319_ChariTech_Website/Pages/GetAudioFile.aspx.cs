using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class GetAudioFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the ExhortationID from the query string
            int exhortationId = int.Parse(Request.QueryString["id"]);

            // Retrieve the audio file from the database
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT AudioFile FROM Exhortation WHERE ExhortationID = @ExhortationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExhortationID", exhortationId);
                    byte[] audioData = (byte[])cmd.ExecuteScalar();

                    if (audioData != null)
                    {
                        // Set the response content type to audio/mpeg
                        Response.ContentType = "audio/mpeg";
                        Response.BinaryWrite(audioData); // Send binary data to the response
                        Response.End(); // End the response
                    }
                    else
                    {
                        Response.StatusCode = 404; // Not found
                        Response.End();
                    }
                }
            }
        }
    }
}