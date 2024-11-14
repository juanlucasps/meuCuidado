﻿using meuCuidado.Dominio.Models;
using meuCuidado.Dominio.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static meuCuidado.Dominio.Extensions.EnumExtension;

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
                Session["SenhaCodificada"] = senha;
                Session["TipoUsuario"] = GetEnumDescription(TipoUsuario.Idoso);

                var autenticacaoViewModel = new AutenticacaoViewModel
                {
                    Codigo1 = string.Empty,
                    Codigo2 = string.Empty,
                    Codigo3 = string.Empty,
                    Codigo4 = string.Empty,
                    Codigo5 = string.Empty,
                    Email = email
                };

                return View("Autenticacao", autenticacaoViewModel);
            }

            ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
            return View();
        }

        public ActionResult Autenticacao(AutenticacaoViewModel autenticacaoViewModel)
        {
            return View(autenticacaoViewModel);
        }

        [HttpPost]
        public ActionResult ReenviarCodigoAutenticacao(string email, string senha, string returnUrl)
        {
            senha = Session["SenhaCodificada"].ToString();
            return Login(email, senha, returnUrl);
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
                return RedirectToAction("Login");

            var email = string.Empty;
            var senha = string.Empty;

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

            var claims = new[] { new Claim(ClaimTypes.Name, email) };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignIn(identity);

            //Realizar login
            authManager.SignIn(identity);

            return RedirectToLocal(returnUrl);
        }

        [HttpPost]
        public ActionResult ValidarCodigoAutenticacao(AutenticacaoViewModel model)
        {
            // Concatenar os códigos em um só
            var codigoInserido = $"{model.Codigo1}{model.Codigo2}{model.Codigo3}{model.Codigo4}{model.Codigo5}";
            // Pega o código da sessão
            var codigoCorreto = Session["CodigoAutenticacao"].ToString();

            if (codigoInserido == codigoCorreto)
            {
                return Json(new { redirectUrl = Url.Action("Dashboard", "Dashboard") });
            }
            else
            {
                return Json(new { success = false, message = "Código de autenticação inválido." });
            }
        }

        [HttpPost]
        public ActionResult FecharPopup()
        {
            ViewBag.ShowPopup = false;
            return View("Login");
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