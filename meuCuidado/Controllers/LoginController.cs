using meuCuidado.Dominio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace meuCuidado.Controllers
{
    public class LoginController : Controller
    {
        private readonly MeuCuidadoDbContext _context = new MeuCuidadoDbContext();
        private readonly EmailController _emailController = new EmailController();

        // Tela de Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        // Método para realizar login e autenticação
        [HttpPost]
        public ActionResult Login(string email, string senha, string returnUrl)
        {
            // verificar uma forma de melhorar isso

            var cuidadorDeIdoso = _context.CuidadoresDeIdoso.FirstOrDefault(p => p.Email == email && p.Senha == senha);
            var fisioterapeuta = _context.Fisioterapeutas.FirstOrDefault(p => p.Email == email && p.Senha == senha);
            var idoso = _context.Idosos.FirstOrDefault(p => p.Email == email && p.Senha == senha);
            var tutor = _context.Tutores.FirstOrDefault(p => p.Email == email && p.Senha == senha);

            if (cuidadorDeIdoso != null || fisioterapeuta != null || idoso != null || tutor != null)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, email) };
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                var authManager = HttpContext.GetOwinContext().Authentication;
                authManager.SignIn(identity);

                var codigoAutenticacao = _emailController.EnviarEmailAutenticacao(email);
                Session["CodigoAutenticacao"] = codigoAutenticacao;

                // Exibir popup com a mensagem de envio do código
                ViewBag.ShowPopup = true;
                ViewBag.PopupMessage = "Código de autenticação enviado para seu e-mail.";

                var autenticacaoViewModel = new AutenticacaoViewModel
                {
                    Codigo1 = string.Empty,
                    Codigo2 = string.Empty,
                    Codigo3 = string.Empty,
                    Codigo4 = string.Empty,
                    Codigo5 = string.Empty,
                    Email = email
                };

                return View(autenticacaoViewModel);
            }

            ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
            return View();
        }


        // Métodos para login com Google
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Solicitar redirecionamento para o provedor de autenticação externa
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        // Método de callback após autenticação externa
        [HttpGet]
        [AllowAnonymous]
        public async System.Threading.Tasks.Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await HttpContext.GetOwinContext().Authentication.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            //var usuario = _context.Pessoas.SingleOrDefault(p => p.Email == loginInfo.Email);
            //if (usuario == null)
            //{
            //    // Se não existir, você pode criar um novo usuário aqui
            //    usuario = new Usuario
            //    {
            //        Email = loginInfo.Email,
            //        // Preencha outros campos necessários
            //    };
            //    _context.Pessoas.Add(usuario);
            //    _context.SaveChanges();
            //}

            // Realizar login
            //var claims = new[] { new Claim(ClaimTypes.Name, usuario.Email) };
            //var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            //var authManager = HttpContext.GetOwinContext().Authentication;
            //authManager.SignIn(identity);

            //return RedirectToLocal(returnUrl);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ValidarCodigoAutenticacao(AutenticacaoViewModel model)
        {
            // Concatenar os códigos em um só
            var codigoInserido = $"{model.Codigo1}{model.Codigo2}{model.Codigo3}{model.Codigo4}{model.Codigo5}";
            // Pega o código da sessão
            var codigoCorreto = Session["CodigoAutenticacao"].ToString();

            if (codigoInserido == codigoCorreto)
                return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Dashboard") });

            // Se o código estiver incorreto, retorna ao login com mensagem de erro
            return Json(new { success = false, message = "Código de autenticação inválido." });
        }


        // Método para redirecionar após o login
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Dashboard");
        }
    }
}