using meuCuidado.Dominio.Models;
using System.Linq;
using System.Web.Mvc;
using static meuCuidado.Dominio.Extensions.EnumExtension;

namespace meuCuidado.Controllers
{
    public class PerfilController : Controller
    {
        private readonly MeuCuidadoDbContext _context = new MeuCuidadoDbContext();

        public ActionResult Perfil()
        {
            var tipoUsuario = Session["TipoUsuario"]?.ToString() ?? string.Empty;

            ViewBag.UsuariosParaExibir = null;
            if (tipoUsuario == GetEnumDescription(TipoUsuario.Cuidador) || tipoUsuario == GetEnumDescription(TipoUsuario.Fisioterapeuta))
            {
                var idosos = _context.Idosos.ToList();
                var familiares = _context.Tutores.ToList();
                if (idosos?.Count() > 0 && familiares.Count() > 0)
                    ViewBag.UsuariosParaExibir = idosos.Concat<Usuario>(familiares);
            }
            else if (tipoUsuario == GetEnumDescription(TipoUsuario.Idoso) || tipoUsuario == GetEnumDescription(TipoUsuario.Tutor))
            {
                var cuidadores = _context.CuidadoresDeIdoso.ToList();
                var fisioterapeutas = _context.Fisioterapeutas.ToList();
                if (cuidadores?.Count() > 0 && fisioterapeutas.Count() > 0)
                    ViewBag.UsuariosParaExibir = cuidadores.Concat<Usuario>(fisioterapeutas);
            }

            return View(new Usuario());
        }

        // Exibe o perfil do usuário com base no ID
        public ActionResult DetalharPerfil(int id)
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