using System;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class DownloadExhortation : System.Web.UI.Page
    {
        private ExhortationManager exhortationManager = new ExhortationManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            int exhortationId;
            if (int.TryParse(Request.QueryString["id"], out exhortationId))
            {
                byte[] audioFile = exhortationManager.GetExhortationAudio(exhortationId);

                if (audioFile != null)
                {
                    try
                    {
                        // Return the audio file directly as a stream
                        Response.Clear();
                        Response.ContentType = "audio/mpeg";
                        Response.OutputStream.Write(audioFile, 0, audioFile.Length);
                        Response.Flush();
                        Response.End();
                    }
                    catch (System.Web.HttpException ex)
                    {
                        // Log the error (if necessary) and handle gracefully
                        Console.WriteLine("Client closed the connection. Error: " + ex.Message);
                    }
                }
            }
        }

    }
}
