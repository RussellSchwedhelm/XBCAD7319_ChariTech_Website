using System;
using System.Web;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class DownloadExhortation : System.Web.UI.Page
    {
        private ExhortationManager exhortationManager = new ExhortationManager();
        private const int BufferSize = 1024 * 16; // 16 KB buffer size for chunked streaming

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["id"], out int exhortationId))
            {
                byte[] audioFile = exhortationManager.GetExhortationAudio(exhortationId);

                if (audioFile != null)
                {
                    StreamAudioFile(audioFile);
                }
                else
                {
                    // Handle not found case
                    Response.StatusCode = 404;
                    Response.StatusDescription = "Audio file not found.";
                    Response.End();
                }
            }
            else
            {
                // Handle invalid ID case
                Response.StatusCode = 400;
                Response.StatusDescription = "Invalid exhortation ID.";
                Response.End();
            }
        }

        private void StreamAudioFile(byte[] audioFile)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "audio/mpeg";
                Response.BufferOutput = false; // Disable output buffering for faster response

                // Stream the audio in smaller chunks
                using (var stream = new System.IO.MemoryStream(audioFile))
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        Response.OutputStream.Write(buffer, 0, bytesRead);
                        Response.Flush(); // Send the data immediately to the client
                    }
                }

                Response.End(); // Ensure the response ends properly
            }
            catch (HttpException ex)
            {
                // Handle cases where the client cancels the download
                Console.WriteLine("Client disconnected: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Log or handle unexpected exceptions
                Console.WriteLine("Error streaming audio: " + ex.Message);
            }
        }

    }
}
