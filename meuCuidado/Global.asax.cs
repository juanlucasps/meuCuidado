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
        // Armazenando o IMapper de forma est�tica
        public static IMapper Mapper { get; private set; }

        protected void Application_Start()
        {
            // Configura��o do AutoMapper
            RegisterMappings();

            // Configura��es do ASP.NET MVC
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
            // Cria a configura��o do AutoMapper
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

            // Cria o IMapper a partir da configura��o e armazena na propriedade est�tica
            Mapper = configuration.CreateMapper();
        }
    }
}
