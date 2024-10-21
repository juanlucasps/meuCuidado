using System;
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
            var lembretes = _context.Lembretes.Include("Medicamento").ToList();
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
                    _context.Lembretes.Add(lembrete);
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) as needed
                    return Json(new { success = false, message = "Erro ao criar lembrete." });
                }
            }
            return Json(new { success = false });
        }

        public JsonResult GetReminders(string date)
        {
            DateTime selectedDate;
            if (!DateTime.TryParse(date, out selectedDate))
            {
                return Json(new { success = false, message = "Data inválida." }, JsonRequestBehavior.AllowGet);
            }

            var lembretes = _context.Lembretes
                .AsNoTracking() // Melhora a performance
                .Where(l => DbFunctions.TruncateTime(l.DataHora) == selectedDate.Date) // Use TruncateTime
                .Select(l => new { l.Descricao, l.DataHora })
                .ToList();

            return Json(lembretes, JsonRequestBehavior.AllowGet);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
