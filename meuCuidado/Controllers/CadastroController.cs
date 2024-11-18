﻿using meuCuidado.Dominio.Models;
using meuCuidado.Dominio.ViewModels;
using Newtonsoft.Json;
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
                        NecessidadesEspeciais = false
                    };

                    TempData["Idoso"] = JsonConvert.SerializeObject(idoso);

                    return RedirectToAction("CadastroTutorEMedicoDoIdoso");
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
                    return CadastroProfissional(pessoa);

                return RedirectToAction("Login", "Login");
            }
            else
                return View(pessoa);
        }

        public ActionResult CadastroTutorEMedicoDoIdoso()
        {
            var idosoJson = TempData["Idoso"] as string;
            if (string.IsNullOrEmpty(idosoJson))
            {
                // Se não encontrar os dados, redireciona de volta para a página anterior
                return RedirectToAction("Cadastro");
            }

            // Desserializa os dados do idoso
            var idoso = JsonConvert.DeserializeObject<Idoso>(idosoJson);

            // Cria um ViewModel para o cadastro de Tutor e Médico
            var viewModel = new CadastroTutorEMedicoDoIdosoViewModel
            {
                Idoso = idoso,
                Tutor = new Tutor(),
                Medicos = new List<Medico>() // Inicializa a lista de médicos
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SalvarCadastroTutorEMedico(CadastroTutorEMedicoDoIdosoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Atualiza o Tutor do Idoso
                if (viewModel.Idoso != null)
                {
                    viewModel.Idoso.Tutor.Email = viewModel.Tutor.Email; // O e-mail passa a ser igual ao do tutor se for diferente;

                    // Adiciona o médico (caso tenha sido inserido)
                    foreach (var medico in viewModel.Medicos)
                    {
                        _context.Medicos.Add(medico);
                    }

                    _context.SaveChanges();

                    return RedirectToAction("Sucesso", "Cadastro"); // Redireciona para uma tela de sucesso
                }
            }

            return View(viewModel); // Retorna a tela de cadastro com erros, caso exista algum
        }



        // Tela de Cadastro do Profissional
        public ActionResult CadastroProfissional(CadastroViewModel pessoa)
        {
            return View("CadastroProfissional", pessoa);
        }

        [HttpPost]
        public ActionResult CadastroProfissional(CadastroViewModel cadastroProfissionalViewModel, HttpPostedFileBase FotoDocumento, HttpPostedFileBase Documento, HttpPostedFileBase CertificadoBonsAntecedentes, HttpPostedFileBase CertificadoDispensa)
        {
            if (ModelState.IsValid)
            {
                int? idUsuario = null;

                if (cadastroProfissionalViewModel.TipoUsuario == TipoUsuario.Cuidador)
                {
                    // MODEL PARA CADASTRO DE IDOSO
                    CuidadorDeIdoso cuidadorDeIdoso = new CuidadorDeIdoso
                    {
                        IdentificadorUnico = Guid.NewGuid(),
                        Nome = cadastroProfissionalViewModel.Usuario.Nome,
                        Email = cadastroProfissionalViewModel.Usuario.Email,
                        CPF = cadastroProfissionalViewModel.Usuario.CPF,
                        Endereco = cadastroProfissionalViewModel.Usuario.Endereco,
                        Telefone = cadastroProfissionalViewModel.Usuario.Telefone,
                        Senha = cadastroProfissionalViewModel.Usuario.Senha,
                        DataCadasto = DateTime.Now
                    };

                    _context.CuidadoresDeIdoso.Add(cuidadorDeIdoso);
                    _context.SaveChanges();
                    idUsuario = cuidadorDeIdoso.Id;
                }
                else if (cadastroProfissionalViewModel.TipoUsuario == TipoUsuario.Fisioterapeuta)
                {
                    // MODEL PARA CADASTRO DE TUTOR
                    Fisioterapeuta fisioterapeuta = new Fisioterapeuta
                    {
                        IdentificadorUnico = new Guid(),
                        Nome = cadastroProfissionalViewModel.Usuario.Nome,
                        Email = cadastroProfissionalViewModel.Usuario.Email,
                        CPF = cadastroProfissionalViewModel.Usuario.CPF,
                        Endereco = cadastroProfissionalViewModel.Usuario.Endereco,
                        Telefone = cadastroProfissionalViewModel.Usuario.Telefone,
                        Senha = cadastroProfissionalViewModel.Usuario.Senha,
                        DataCadasto = DateTime.Now
                    };

                    _context.Fisioterapeutas.Add(fisioterapeuta);
                    _context.SaveChanges();
                    idUsuario = fisioterapeuta.Id;
                }

                if (idUsuario != null && idUsuario != 0)
                {
                    SalvarDocumento(FotoDocumento, TipoDocumento.FotoDocumento, cadastroProfissionalViewModel.Usuario.Id);
                    SalvarDocumento(Documento, TipoDocumento.Documento, cadastroProfissionalViewModel.Usuario.Id);
                    SalvarDocumento(CertificadoBonsAntecedentes, TipoDocumento.CertificadoBonsAntecedentes, cadastroProfissionalViewModel.Usuario.Id);
                    SalvarDocumento(CertificadoDispensa, TipoDocumento.CertificadoDispensa, cadastroProfissionalViewModel.Usuario.Id);
                }

                return RedirectToAction("Login", "Login", cadastroProfissionalViewModel.Usuario);
            }

            // Pega os erros da model
            var errors = GetModelErrors();

            // Aqui você pode fazer algo com os erros, como logar ou enviar para a view
            ViewBag.Errors = errors;

            return View(cadastroProfissionalViewModel);
        }

        
        private void SalvarDocumento(HttpPostedFileBase arquivo, TipoDocumento tipoDocumento, int usuarioId)
        {
            if (arquivo != null && arquivo.ContentLength > 0)
            {
                // Verificar a extensão do arquivo
                var extensao = Path.GetExtension(arquivo.FileName).ToLower();
                TipoExtensaoDocumento tipoExtensao;

                switch (extensao)
                {
                    case ".jpg":
                    case ".jpeg":
                        tipoExtensao = TipoExtensaoDocumento.JPG;
                        break;
                    case ".png":
                        tipoExtensao = TipoExtensaoDocumento.PNG;
                        break;
                    case ".pdf":
                        tipoExtensao = TipoExtensaoDocumento.PDF;
                        break;
                    case ".docx":
                        tipoExtensao = TipoExtensaoDocumento.DOCX;
                        break;
                    case ".xlsx":
                        tipoExtensao = TipoExtensaoDocumento.XLSX;
                        break;
                    default:
                        throw new InvalidOperationException("Tipo de arquivo não suportado.");
                }

                // Caminho onde o arquivo será salvo
                var caminho = Server.MapPath("~/DocumentosAnalise/");

                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho);

                var nomeArquivo = Guid.NewGuid() + extensao;
                var caminhoCompleto = Path.Combine(caminho, nomeArquivo);

                // Salvar o arquivo no servidor
                arquivo.SaveAs(caminhoCompleto);

                // Criar a instância do documento e associá-lo ao usuário
                var documento = new Documento
                {
                    Id = Guid.NewGuid(),
                    TipoDocumento = tipoDocumento,
                    Extensao = tipoExtensao,
                    Caminho = caminhoCompleto,
                    UsuarioId = usuarioId
                };

                //_context.Documentos.Add(documento);
                _context.SaveChanges();
            }
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