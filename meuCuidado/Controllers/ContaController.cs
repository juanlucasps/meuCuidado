using System.Data.Entity;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using meuCuidado.Models;
using System.Linq;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using static meuCuidado.Extensions.EnumExtension;
using System.Collections.Generic;
using System;

namespace meuCuidado.Controllers
{
    public class ContaController : Controller
    {
        private readonly MeuCuidadoDbContext _context = new MeuCuidadoDbContext();

        // Tela de Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // Tela de Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // Tela de Cadastro do Profissional
        public ActionResult CadastroProfissional()
        {
            return View();
        }

        // Dashboard
        public ActionResult Dashboard()
        {
            var usuarioLogado = User.Identity.Name;
            //var pessoa = _context.Usuarios.SingleOrDefault(p => p.Email == usuarioLogado);

            //if (pessoa == null)
            //{
            //    return RedirectToAction("Login");
            //}

            // Verifica se a pessoa é CuidadorDeIdoso ou Fisioterapeuta
            //if (pessoa is CuidadorDeIdoso || pessoa is Fisioterapeuta)
            //{
            //    var idosos = _context.Idosos.ToList();
            //    var familiares = _context.Familiares.ToList();
            //    ViewBag.UsuariosParaExibir = idosos.Concat<Usuario>(familiares);
            //}
            // Verifica se a pessoa é Idoso ou Familiar
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

        // Método para realizar login e autenticação
        [HttpPost]
        public ActionResult Login(string email, string senha, string returnUrl)
        {
            // verificar uma forma de melhorar isso

            var cuidadorDeIdoso = _context.CuidadoresDeIdoso.SingleOrDefault(p => p.Email == email && p.Senha == senha);
            var fisioterapeuta = _context.Fisioterapeutas.SingleOrDefault(p => p.Email == email && p.Senha == senha);
            var idoso = _context.Idosos.SingleOrDefault(p => p.Email == email && p.Senha == senha);
            var tutor = _context.Tutores.SingleOrDefault(p => p.Email == email && p.Senha == senha);

            if (cuidadorDeIdoso != null || fisioterapeuta != null || idoso != null || tutor != null)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, email) };
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                var authManager = HttpContext.GetOwinContext().Authentication;
                authManager.SignIn(identity);

                return RedirectToLocal(returnUrl);
            }

            ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
            return View();
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

        // Método para realizar cadastro
        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel pessoa)
        {
            if (ModelState.IsValid)
            {
                if (pessoa.TipoUsuario == TipoUsuario.Idoso)
                {
                    // MODEL PARA CADASTRO DE IDOSO
                    Idoso idoso = new Idoso
                    {
                        IdentificadorUnico = Guid.NewGuid(),
                        Nome = pessoa.Usuario.Nome,
                        Email = pessoa.Usuario.Email,
                        CPF = pessoa.Usuario.CPF,
                        Endereco = pessoa.Usuario.Endereco,
                        Telefone = pessoa.Usuario.Telefone,
                        Senha = pessoa.Usuario.Senha, 
                        DataCadasto = DateTime.Now, 
                        DataNascimento = DateTime.Now, 
                        NecessidadesEspeciais = false,
                        Tutor = new Tutor {
                            IdentificadorUnico = Guid.NewGuid(),
                            Nome = pessoa.Usuario.Nome,
                            Email = pessoa.Usuario.Email,
                            CPF = pessoa.Usuario.CPF,
                            Endereco = pessoa.Usuario.Endereco,
                            Telefone = pessoa.Usuario.Telefone,
                            Senha = pessoa.Usuario.Senha,
                            DataCadasto = DateTime.Now,
                            RelacaoComIdoso = TipoUsuario.Tutor.ToString(),
                            IdadeDoIdoso = DateTime.Now,
                            NecessidadesEspeciais = false,
                        }
                    };

                    _context.Idosos.Add(idoso);
                    _context.SaveChanges();
                }
                else if (pessoa.TipoUsuario == TipoUsuario.Tutor)
                {
                    // MODEL PARA CADASTRO DE TUTOR
                    Tutor tutor = new Tutor
                    {
                        IdentificadorUnico = new Guid(),
                        Nome = pessoa.Usuario.Nome,
                        Email = pessoa.Usuario.Email,
                        CPF = pessoa.Usuario.CPF,
                        Endereco = pessoa.Usuario.Endereco,
                        Telefone = pessoa.Usuario.Telefone,
                        Senha = pessoa.Usuario.Senha,
                        DataCadasto = DateTime.Now,
                        RelacaoComIdoso = "Tutor",
                        IdadeDoIdoso = DateTime.Now,
                        NecessidadesEspeciais = false
                    };

                    _context.Tutores.Add(tutor);
                    _context.SaveChanges();
                }
                else
                {
                    return RedirectToAction("CadastroProfissional");
                }

                return RedirectToAction("Login");
            }

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
    }
}
