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

            // configurar as demais rotas

            routes.MapRoute(
                name: "Lembrete",
                url: "{controller}/{action}",
                defaults: new { controller = "Lembrete", action = "Lembrete" }
            );
        }
    }
}
