using AutoMapper;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using meuCuidado.Dominio.Models;
using System.Web.Optimization;

namespace meuCuidado
{
    public class MvcApplication : HttpApplication
    {
        // Armazenando o IMapper de forma estática
        public static IMapper Mapper { get; private set; }

        protected void Application_Start()
        {
            // Configuração do AutoMapper
            RegisterMappings();

            // Configurações do ASP.NET MVC
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            if (HttpContext.Current.Request.Url.AbsolutePath == "/")
            {
                HttpContext.Current.Response.Redirect("/Login/Login");
            }
        }

        /// <summary>
        /// Registra os mapeamentos do AutoMapper
        /// </summary>
        private void RegisterMappings()
        {
            // Cria a configuração do AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tutor, Usuario>();
                cfg.CreateMap<Usuario, Tutor>();
                cfg.CreateMap<Idoso, Usuario>();
                cfg.CreateMap<Usuario, Idoso>();
                cfg.CreateMap<CuidadorDeIdoso, Usuario>();
                cfg.CreateMap<Usuario, CuidadorDeIdoso>();
                cfg.CreateMap<Fisioterapeuta, Usuario>();
                cfg.CreateMap<Usuario, Fisioterapeuta>();
            });

            // Cria o IMapper a partir da configuração e armazena na propriedade estática
            Mapper = configuration.CreateMapper();
        }
    }
}
