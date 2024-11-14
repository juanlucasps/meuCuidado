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
            var lembretes = new List<Lembrete>();
            return View(lembretes);
        }

        [HttpPost]
        public JsonResult Create(Lembrete lembrete)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    lembrete.IdentificadorUnico = Guid.NewGuid();
                    lembrete.RelacionamentoIdosoProfissional = new RelacionamentoIdosoProfissional();
                    lembrete.RelacionamentoIdosoProfissionalId = 0;
                    lembrete.Repete = false;
                    _context.Lembretes.Add(lembrete);
                    _context.SaveChanges();
                    TempData["Success"] = "Lembrete adicionado com sucesso!";
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Erro ao adicionar lembrete.";
                    return Json(new { success = false, message = "Erro ao criar lembrete." });
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
                .AsNoTracking()
                .Where(l => DbFunctions.TruncateTime(l.DataHora) == selectedDate.Date)
                .ToList();

            return PartialView("ListaLembretes", lembretes);
        }

        public ActionResult Edit(int id)
        {
            var lembrete = _context.Lembretes.Find(id);
            if (lembrete == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicamentoId = new SelectList(_context.Medicamentos, "Id", "Nome", lembrete.MedicamentoId);
            return View(lembrete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lembrete lembrete)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(lembrete).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicamentoId = new SelectList(_context.Medicamentos, "Id", "Nome", lembrete.MedicamentoId);
            return View(lembrete);
        }

        public ActionResult Delete(int id)
        {
            var lembrete = _context.Lembretes.Find(id);
            if (lembrete == null)
            {
                return HttpNotFound();
            }
            return View(lembrete);
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
