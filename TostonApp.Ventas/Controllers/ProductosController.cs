using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TostonDbContext _context;
        public ProductosController(TostonDbContext context) => _context = context;

        // LISTADO
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos
                .Where(p => p.Activo)
                .OrderBy(p => p.Nombre)
                .ToListAsync());
        }

        // CREAR
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Activo = true;
            _context.Productos.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // EDITAR (controla stock)
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto model)
        {
            if (id != model.ID_Producto) return NotFound();
            if (!ModelState.IsValid) return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR (SOFT)
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            producto.Activo = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
