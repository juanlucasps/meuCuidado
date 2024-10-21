using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Security.Claims;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(MeuCuidado.Startup))]

namespace MeuCuidado
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            // Configuração do middleware de autenticação
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login") // URL para a página de login
            });

            //// Configurar autenticação via Google
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            //{
            //    ClientId = "SEU_CLIENT_ID",
            //    ClientSecret = "SEU_CLIENT_SECRET"
            //});

            //// Configurar autenticação via Facebook
            //app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            //{
            //    AppId = "SEU_APP_ID",
            //    AppSecret = "SEU_APP_SECRET"
            //});
        }
    }
}
