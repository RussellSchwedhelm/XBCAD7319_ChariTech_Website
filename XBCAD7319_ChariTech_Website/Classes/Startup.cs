using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

[assembly: OwinStartup(typeof(XBCAD7319_ChariTech_Website.Startup))]

namespace XBCAD7319_ChariTech_Website
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Enable Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Pages/Login.aspx")
            });

            // Enable Google Authentication
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["GoogleClientId"],
                ClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"],
                SignInAsAuthenticationType = "ApplicationCookie",
                CallbackPath = new PathString("/Pages/GoogleCallback.aspx"),
                Scope = { "https://www.googleapis.com/auth/userinfo.email", "https://www.googleapis.com/auth/userinfo.profile" },

                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnApplyRedirect = context =>
                    {
                        string redirectUri = context.RedirectUri;
                        redirectUri += "&access_type=offline";
                        context.Response.Redirect(redirectUri);
                    },
                    OnAuthenticated = async context =>
                    {
                        // Log the authentication details
                        Debug.WriteLine("Google authentication successful for user: " + context.Email);
                        context.Identity.AddClaim(new System.Security.Claims.Claim("picture", context.User["picture"].ToString()));
                    }
                }
            });
        }
    }
}
