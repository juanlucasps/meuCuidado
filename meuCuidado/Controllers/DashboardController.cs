using meuCuidado.Dominio.Models;
using System.Web.Mvc;

namespace meuCuidado.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Dashboard()
        {
            var usuarioLogado = User.Identity.Name;
            return View(new Usuario());
        }
    }
}