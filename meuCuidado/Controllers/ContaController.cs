using System.Data.Entity;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using meuCuidado.Models;
using System.Linq;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace meuCuidado.Controllers
{
    public class ContaController : Controller
    {
        private readonly AplicacaoDbContext _context = new AplicacaoDbContext();

        // Tela de Login
        public ActionResult Login()
        {
            return View();
        }

        // Tela de Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // Dashboard
        public ActionResult Dashboard()
        {
            var usuarioLogado = User.Identity.Name;
            var pessoa = _context.Pessoas.SingleOrDefault(p => p.Email == usuarioLogado);

            if (pessoa == null)
            {
                return RedirectToAction("Login");
            }

            // Verifica se a pessoa é CuidadorDeIdoso ou Fisioterapeuta
            if (pessoa is CuidadorDeIdoso || pessoa is Fisioterapeuta)
            {
                var idosos = _context.Idosos.ToList();
                var familiares = _context.Familiares.ToList();
                ViewBag.UsuariosParaExibir = idosos.Concat<Pessoa>(familiares);
            }
            // Verifica se a pessoa é Idoso ou Familiar
            else if (pessoa is Idoso || pessoa is Familiar)
            {
                var cuidadores = _context.Cuidadores.ToList();
                var fisioterapeutas = _context.Fisioterapeutas.ToList();
                ViewBag.UsuariosParaExibir = cuidadores.Concat<Pessoa>(fisioterapeutas);
            }

            return View();
        }


        // Exibe o perfil do usuário com base no ID
        public ActionResult Perfil(int id)
        {
            var usuario = _context.Pessoas.SingleOrDefault(p => p.Id == id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // Método para realizar login e autenticação
        [HttpPost]
        public ActionResult Login(string email, string senha)
        {
            var usuario = _context.Pessoas.SingleOrDefault(p => p.Email == email && p.Senha == senha);
            if (usuario != null)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, email) };
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                var authManager = HttpContext.GetOwinContext().Authentication;
                authManager.SignIn(identity);

                return RedirectToAction("Dashboard");
            }

            ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
            return View();
        }

        // Método para realizar cadastro
        [HttpPost]
        public ActionResult Cadastro(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View();
        }
    }
}
