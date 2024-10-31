using System.Web;
using XBCAD7319_ChariTech_Website.Classes;

public class MarkNotificationsAsRead : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        
        if (context.Session["UserID"] != null)
        {
            int userId = (int)context.Session["UserID"];
            NotificationManager notificationManager = new NotificationManager();

            bool success = notificationManager.MarkNotificationsAsRead(userId);
            context.Response.Write("{ \"success\": " + success.ToString().ToLower() + " }");
        }
        else
        {
            context.Response.Write("{ \"success\": false }");
        }
    }

    public bool IsReusable => false;
}
