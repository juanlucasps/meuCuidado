using System.Web.Mvc;

namespace meuCuidado.Controllers
{
    public class CuidadorController : Controller
    {
        public ActionResult Perfil()
        {
            TempData["Info"] = "Visualizando o perfil do cuidador.";
            return View();
        }

        public ActionResult Avaliacoes()
        {
            TempData["Info"] = "Visualizando as avaliações do cuidador.";
            return View();
        }

        public ActionResult Curriculo()
        {
            TempData["Info"] = "Visualizando o currículo do cuidador.";
            return View();
        }
    }
}