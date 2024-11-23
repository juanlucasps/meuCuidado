using System.Data.Entity;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using meuCuidado.Dominio.Models;
using System.Linq;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using static meuCuidado.Dominio.Extensions.EnumExtension;
using System.Collections.Generic;
using System;
using System.IO;
using meuCuidado.Dominio.ViewModels;

namespace meuCuidado.Controllers
{
    public class ContaController : Controller
    {
        private readonly MeuCuidadoDbContext _context = new MeuCuidadoDbContext();
        private readonly EmailController _emailController = new EmailController();

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

        [HttpPost]
        //public ActionResult CadastroProfissional(Usuario model, HttpPostedFileBase FotoDocumento, HttpPostedFileBase Documento, HttpPostedFileBase CertificadoBonsAntecedentes, HttpPostedFileBase CertificadoDispensa)
        public ActionResult CadastroProfissional(CadastroProfissionalViewModel cadastroProfissionalViewModel, HttpPostedFileBase FotoDocumento, HttpPostedFileBase Documento, HttpPostedFileBase CertificadoBonsAntecedentes, HttpPostedFileBase CertificadoDispensa)
        {
            if (ModelState.IsValid)
            {
                // Aqui você pode salvar as informações do profissional
                // Verifique se cada arquivo foi enviado e faça o upload
                if (FotoDocumento != null && FotoDocumento.ContentLength > 0)
                {
                    var caminhoFotoDocumento = Server.MapPath("~/DocumentosAnalise/FotosDocumento/");
                    if (!Directory.Exists(caminhoFotoDocumento))
                        Directory.CreateDirectory(caminhoFotoDocumento);

                    var nomeArquivoFoto = Guid.NewGuid() + Path.GetExtension(FotoDocumento.FileName);
                    FotoDocumento.SaveAs(caminhoFotoDocumento + nomeArquivoFoto);
                }

                if (Documento != null && Documento.ContentLength > 0)
                {
                    var caminhoDocumento = Server.MapPath("~/DocumentosAnalise/Documentos/");
                    if (!Directory.Exists(caminhoDocumento))
                        Directory.CreateDirectory(caminhoDocumento);

                    var nomeArquivoDocumento = Guid.NewGuid() + Path.GetExtension(Documento.FileName);
                    Documento.SaveAs(caminhoDocumento + nomeArquivoDocumento);
                }

                if (CertificadoBonsAntecedentes != null && CertificadoBonsAntecedentes.ContentLength > 0)
                {
                    var caminhoCertificadoBonsAntecedentes = Server.MapPath("~/DocumentosAnalise/CertificadosBonsAntecedentes/");
                    if (!Directory.Exists(caminhoCertificadoBonsAntecedentes))
                        Directory.CreateDirectory(caminhoCertificadoBonsAntecedentes);

                    var nomeArquivoCertificado = Guid.NewGuid() + Path.GetExtension(CertificadoBonsAntecedentes.FileName);
                    CertificadoBonsAntecedentes.SaveAs(caminhoCertificadoBonsAntecedentes + nomeArquivoCertificado);
                }

                if (CertificadoDispensa != null && CertificadoDispensa.ContentLength > 0)
                {
                    var caminhoCertificadoDispensa = Server.MapPath("~/DocumentosAnalise/CertificadosDispensa/");
                    if (!Directory.Exists(caminhoCertificadoDispensa))
                        Directory.CreateDirectory(caminhoCertificadoDispensa);

                    var nomeArquivoDispensa = Guid.NewGuid() + Path.GetExtension(CertificadoDispensa.FileName);
                    CertificadoDispensa.SaveAs(caminhoCertificadoDispensa + nomeArquivoDispensa);
                }

                return RedirectToAction("Dashboard");
            }

            // Pega os erros da model
            var errors = GetModelErrors();

            // Aqui você pode fazer algo com os erros, como logar ou enviar para a view
            ViewBag.Errors = errors;

            return View(cadastroProfissionalViewModel);
        }

        private List<string> GetModelErrors()
        {
            var errors = new List<string>();

            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    errors.Add($"{modelState.Key}: {error.ErrorMessage}");
                }
            }

            return errors;
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

        [HttpPost]
        public ActionResult ValidarCodigoAutenticacao(AutenticacaoViewModel model)
        {
            // Concatenar os códigos em um só
            var codigoInserido = $"{model.Codigo1}{model.Codigo2}{model.Codigo3}{model.Codigo4}{model.Codigo5}";
            // Pega o código da sessão
            var codigoCorreto = Session["CodigoAutenticacao"].ToString();

            if (codigoInserido == codigoCorreto)
                return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Conta") });

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
                        NecessidadesEspeciais = false
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
