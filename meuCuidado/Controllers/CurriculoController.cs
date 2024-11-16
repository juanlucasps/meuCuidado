using meuCuidado.Dominio.Models;
using System.Web.Mvc;

namespace meuCuidado.Controllers
{
    public class CurriculoController : Controller
    {
        public ActionResult Curriculo()
        {
            return View(new Curriculo());
        }

        public ActionResult EditarCurriculo()
        {
            return View(new Curriculo());
        }
    }
}