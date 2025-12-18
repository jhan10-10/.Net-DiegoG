using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    public class DomiciliosController : Controller
    {
        private readonly TostonDbContext _context;
        public DomiciliosController(TostonDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            return View(await _context.Domicilios
                .Include(d => d.Venta)
                .ToListAsync());
        }

        public IActionResult Crear(int idVenta)
        {
            return View(new Domicilio { ID_venta = idVenta });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Domicilio model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Fecha = DateTime.Now;
            model.Activo = true;

            _context.Domicilios.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
