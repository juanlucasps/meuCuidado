using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using meuCuidado.Dominio.Models;

namespace meuCuidado.Controllers
{
    public class LembreteController : Controller
    {
        private readonly MeuCuidadoDbContext _context;

        public LembreteController()
        {
            _context = new MeuCuidadoDbContext();
        }

        public ActionResult Lembrete()
        {
            var medicamentos = _context.Medicamentos.ToList();
            ViewBag.Medicamentos = medicamentos; // Envia lista de medicamentos para a view
            var lembretes = new List<Lembrete>();
            return View(lembretes);
        }

        [HttpPost]
        public JsonResult Create(Lembrete lembrete) // TODO: Pegar a Dosagem do Medicamento
        {
            if (ModelState.IsValid)
            {
                try
                {
                    lembrete.IdentificadorUnico = Guid.NewGuid();
                    lembrete.RelacionamentoIdosoProfissional = new RelacionamentoIdosoProfissional();
                    lembrete.RelacionamentoIdosoProfissionalId = 1; // Definir de acordo com a lógica do projeto

                    _context.Lembretes.Add(lembrete);
                    _context.SaveChanges();

                    return Json(new { success = true });
                }
                catch (Exception)
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false });
        }

        public ActionResult GetReminders(string date)
        {
            DateTime selectedDate;
            if (!DateTime.TryParse(date, out selectedDate))
            {
                return Json(new { success = false, message = "Data inválida." }, JsonRequestBehavior.AllowGet);
            }

            var lembretes = _context.Lembretes
                        .Where(l => DbFunctions.TruncateTime(l.DataHora) == selectedDate.Date).Include(l => l.Medicamento) // Incluir o medicamento, se houver
                        .ToList();

            return PartialView("ListaLembretes", lembretes);
        }

        public ActionResult Excluir(int id)
        {
            var lembrete = _context.Lembretes.Find(id);

            // Verifica se o lembrete existe
            if (lembrete == null)
            {
                return HttpNotFound(); // Retorna erro 404 se não encontrar
            }

            // Remove o lembrete
            _context.Lembretes.Remove(lembrete);

            // Salva as alterações no banco
            _context.SaveChanges();

            return RedirectToAction("Dashboard", "Dashboard");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var lembrete = _context.Lembretes.Find(id);
            _context.Lembretes.Remove(lembrete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
