using Microsoft.AspNetCore.Mvc;
using TostonApp.Ventas.Models;
using Microsoft.EntityFrameworkCore;

namespace TostonApp.Ventas.Controllers
{
    public class AgendaController : Controller
    {
        private readonly TostonDbContext _context;

        public AgendaController(TostonDbContext context)
        {
            _context = context;
        }

        // =========================
        // LISTAR
        // =========================
        public IActionResult Index()
        {
            var agenda = _context.Agenda.ToList();
            return View(agenda);
        }

        // =========================
        // CREAR
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Agenda model)
        {
            if (ModelState.IsValid)
            {
                _context.Agenda.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // =========================
        // EDITAR
        // =========================
        public IActionResult Edit(int id)
        {
            var item = _context.Agenda.Find(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Agenda model)
        {
            if (ModelState.IsValid)
            {
                _context.Agenda.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // =========================
        // ELIMINAR
        // =========================
        public IActionResult Delete(int id)
        {
            var item = _context.Agenda.Find(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _context.Agenda.Find(id);

            if (item == null)
                return NotFound();

            _context.Agenda.Remove(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DETALLES
        // =========================
        public IActionResult Details(int id)
        {
            var item = _context.Agenda.Find(id);

            if (item == null)
                return NotFound();

            return View(item);
        }
    }
}
    