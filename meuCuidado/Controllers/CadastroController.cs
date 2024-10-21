using meuCuidado.Dominio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using static meuCuidado.Dominio.Extensions.EnumExtension;

namespace meuCuidado.Controllers
{
    public class CadastroController : Controller
    {
        private readonly MeuCuidadoDbContext _context = new MeuCuidadoDbContext();

        // Tela de Cadastro
        public ActionResult Cadastro()
        {
            return View();
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
                        Tutor = new Tutor
                        {
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

                return RedirectToAction("Dashboard", "Dashboard");
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
    }
}