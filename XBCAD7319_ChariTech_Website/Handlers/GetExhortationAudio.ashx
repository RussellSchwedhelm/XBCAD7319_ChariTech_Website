using System;
using System.Web;
using XBCAD7319_ChariTech_Website.Classes;

public class GetExhortationAudio : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        int exhortationId;
        if (int.TryParse(context.Request.QueryString["id"], out exhortationId))
        {
            ExhortationManager exhortationManager = new ExhortationManager();
            byte[] audioData = exhortationManager.GetExhortationAudio(exhortationId);

            if (audioData != null)
            {
                context.Response.ContentType = "audio/mpeg";
                context.Response.BinaryWrite(audioData);
                context.Response.End();
            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "Audio not found";
            }
        }
        else
        {
            context.Response.StatusCode = 400;
            context.Response.StatusDescription = "Invalid Exhortation ID";
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
