using System.Web.Mvc;

namespace meuCuidado.Controllers
{
    public class PerfilController : Controller
    {
        // Exibe o perfil do usuário com base no ID
        public ActionResult Perfil(int id)
        {
            //var usuario = _context.Pessoas.SingleOrDefault(p => p.Id == id);
            //if (usuario == null)
            //{
            //    return HttpNotFound();
            //}

            return View();
            //return View(usuario);
        }
    }
}