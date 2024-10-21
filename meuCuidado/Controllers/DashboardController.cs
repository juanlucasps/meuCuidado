using meuCuidado.Dominio.Models;
using System.Web.Mvc;
using static meuCuidado.Dominio.Extensions.EnumExtension;

namespace meuCuidado.Controllers
{
    public class DashboardController : Controller
    {
        private readonly MeuCuidadoDbContext _context = new MeuCuidadoDbContext();

        // Dashboard
        public ActionResult Dashboard(TipoUsuario tipoUsuario)
        {
            var usuarioLogado = User.Identity.Name;
            //var pessoa = _context.Usuarios.SingleOrDefault(p => p.Email == usuarioLogado);

            //if (pessoa == null)
            //{
            //    return RedirectToAction("Login");
            //}

            //Verifica se a pessoa é CuidadorDeIdoso ou Fisioterapeuta
            //if (pessoa is CuidadorDeIdoso || pessoa is Fisioterapeuta)
            //{
            //    var idosos = _context.Idosos.ToList();
            //    var familiares = _context.Familiares.ToList();
            //    ViewBag.UsuariosParaExibir = idosos.Concat<Usuario>(familiares);
            //}
            //Verifica se a pessoa é Idoso ou Familiar
            //else if (pessoa is Idoso || pessoa is Familiar)
            //{
            //    var cuidadores = _context.Cuidadores.ToList();
            //    var fisioterapeutas = _context.Fisioterapeutas.ToList();
            //    ViewBag.UsuariosParaExibir = cuidadores.Concat<Usuario>(fisioterapeutas);
            //}

            //var pessoas = _context.Usuarios.ToList();
            //ViewBag.UsuariosParaExibir = pessoas;

            return View();
        }
    }
}