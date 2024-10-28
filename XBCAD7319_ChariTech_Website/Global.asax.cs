using System;
using System.Web;
using System.Web.Routing;
using System.Web.Security;

namespace XBCAD7319_ChariTech_Website
{ 
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Map the default route to Login.aspx
            RouteTable.Routes.MapPageRoute("DefaultRoute", "", "~/Pages/Login.aspx");

            RouteTable.Routes.MapPageRoute("GoogleLogin", "GoogleLogin", "~/Pages/LoginExternal.aspx?provider=Google");
            RouteTable.Routes.MapPageRoute("FacebookLogin", "FacebookLogin", "~/Pages/LoginExternal.aspx?provider=Facebook");
            RouteTable.Routes.MapPageRoute("RegisterRoute", "Register", "~/Pages/Register.aspx");
            RouteTable.Routes.MapPageRoute("DashboardRoute", "Dashboard", "~/Pages/Dashboard.aspx");



        }
    }
}