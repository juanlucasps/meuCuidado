using meuCuidado.Dominio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace meuCuidado.Controllers
{
    public class ConfiguracoesController : Controller
    {
        public ActionResult Configuracoes()
        {
            var configuracoes = new List<ConfiguracoesViewModel>
            {
                // TODO: ALTERAR AS ROTAS E REMOVER AS VIEWS
                new ConfiguracoesViewModel { Titulo = "Perfil", Descricao = "Editar informações do perfil", Link = Url.Action("EditarPerfil", "Perfil") },
                new ConfiguracoesViewModel { Titulo = "Currículo", Descricao = "Editar ou visualizar currículo", Link = Url.Action("EditarCurriculo", "Curriculo") },
                new ConfiguracoesViewModel { Titulo = "Avaliações", Descricao = "Visualizar avaliações recebidas", Link = Url.Action("Avaliacoes", "Configuracoes") }
            };

            return View(configuracoes);
        }

        public ActionResult Perfil()
        {
            // Código para obter dados do perfil
            return View();
        }

        public ActionResult Curriculo()
        {
            // Código para obter dados do currículo
            return View();
        }

        public ActionResult Avaliacoes()
        {
            // Código para obter avaliações
            return View();
        }
    }
}
