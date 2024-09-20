using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(MeuCuidado.Startup))]

namespace MeuCuidado
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configuração do middleware de autenticação
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Conta/Login") // URL para a página de login
            });

            // Outras configurações, se houver
        }
    }
}
