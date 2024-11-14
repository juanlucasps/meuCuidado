using System.Web.Mvc;
using System.Web.Routing;

namespace meuCuidado
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Lembrete",
                url: "{controller}/{action}",
                defaults: new { controller = "Lembrete", action = "Lembrete" }
            );

            routes.MapRoute(
              name: "CriarConta",
              url: "{controller}/{action}",
              defaults: new { controller = "Login", action = "CriarConta" }
           );

           routes.MapRoute(
               name: "Dashboard",
               url: "{controller}/{action}",
               defaults: new { controller = "Dashboard", action = "Dashboard" }
           );

           routes.MapRoute(
               name: "Configuracoes",
               url: "{controller}/{action}",
               defaults: new { controller = "Configuracoes", action = "Configuracoes" }
           );
        }
    }
}
