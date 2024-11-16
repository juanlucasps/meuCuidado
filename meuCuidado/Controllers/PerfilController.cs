using meuCuidado.Dominio.Models;
using meuCuidado.Dominio.ViewModels;
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

        public ActionResult PerfilDetalhado(int id)
        {
            var perfilDetalhado = new PerfilDetalhadoViewModel()
            {
                CuidadorDeIdoso = _context.CuidadoresDeIdoso.SingleOrDefault(p => p.Id == id),
                Fisioterapeuta = _context.Fisioterapeutas.SingleOrDefault(p => p.Id == id)
            };

            if (perfilDetalhado.CuidadorDeIdoso == null && perfilDetalhado.Fisioterapeuta == null)
            {
                return HttpNotFound();
            }
            else
            {
                perfilDetalhado.Curriculo = _context.Curriculos.SingleOrDefault(p => p.Id == id); // colocar relação com usuário
            }

            return View(perfilDetalhado);
        }

        public ActionResult EditarPerfil(int id)
        {
            var cuidadorDeIdoso = _context.CuidadoresDeIdoso.SingleOrDefault(p => p.Id == id);
            var fisioterapeuta = _context.Fisioterapeutas.SingleOrDefault(p => p.Id == id);

            //if (cuidadorDeIdoso == null && fisioterapeuta == null)
            //{
            //    return HttpNotFound();
            //}

            // TODO: aplicar regra para visualizar perfil

            return View(); // TODO: Passar todos os valores
        }
    }
}