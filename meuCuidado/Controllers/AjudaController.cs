using meuCuidado.Dominio.Models;
using System.Web.Mvc;

namespace meuCuidado.Controllers
{
    public class AjudaController : Controller
    {
        public ActionResult Ajuda()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajuda(Ajuda ajuda)
        {
            if (ModelState.IsValid)
            {
                var emailController = new EmailController();
                emailController.EnviarEmail(ajuda);

                ViewBag.Message = "Sua solicitação foi enviada com sucesso!";
                return View();
            }

            return View(ajuda);
        }
    }
}