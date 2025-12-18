using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    [Route("Carrito")]
    public class CarritoController : Controller
    {
        private readonly TostonDbContext _context;
        public CarritoController(TostonDbContext context) => _context = context;

        private async Task CargarProductosAsync(int? seleccionado = null)
        {
            ViewBag.Productos = await _context.Productos
                .Where(p => p.Activo && p.Stock > 0)
                .OrderBy(p => p.Nombre)
                .Select(p => new SelectListItem
                {
                    Value = p.ID_Producto.ToString(),
                    Text = $"{p.Nombre} | $ {p.Precio:N2} | Stock: {p.Stock}",
                    Selected = seleccionado.HasValue && p.ID_Producto == seleccionado.Value
                })
                .ToListAsync();
        }

        private async Task CargarClientesAsync(int? seleccionado = null)
        {
            ViewBag.Clientes = await _context.Usuarios
                .Where(u => u.Activo)
                .OrderBy(u => u.Apellido)
                .ThenBy(u => u.Nombre)
                .Select(u => new SelectListItem
                {
                    Value = u.ID_Usuario.ToString(),
                    Text = $"{u.Nombre} {u.Apellido} - {u.Cedula}",
                    Selected = seleccionado.HasValue && u.ID_Usuario == seleccionado.Value
                })
                .ToListAsync();
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var items = await _context.CarritoCompra
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .Where(c => c.Activo)
                .OrderByDescending(c => c.FechaAgregado)
                .AsNoTracking()
                .ToListAsync();

            // Cargar clientes para el dropdown al crear pedido
            await CargarClientesAsync();

            return View("Index", items);
        }

        [HttpGet("Crear")]
        public async Task<IActionResult> Crear()
        {
            await CargarProductosAsync();
            await CargarClientesAsync();
            return View("Crear", new CarritoCompra { Cantidad = 1 });
        }

        [HttpPost("Crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CarritoCompra model)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.ID_Producto == model.ID_Producto && p.Activo);
            if (producto == null) ModelState.AddModelError(nameof(model.ID_Producto), "Producto inválido.");
            if (model.Cantidad <= 0) ModelState.AddModelError(nameof(model.Cantidad), "Cantidad inválida.");
            if (producto != null && model.Cantidad > producto.Stock)
                ModelState.AddModelError(nameof(model.Cantidad), $"Stock insuficiente. Disponible: {producto.Stock}");

            // Validar cliente si se proporcionó
            if (model.ID_Usuario.HasValue)
            {
                var usuario = await _context.Usuarios.FindAsync(model.ID_Usuario.Value);
                if (usuario == null || !usuario.Activo)
                    ModelState.AddModelError(nameof(model.ID_Usuario), "Cliente no válido.");
            }

            if (!ModelState.IsValid)
            {
                await CargarProductosAsync(model.ID_Producto);
                await CargarClientesAsync(model.ID_Usuario);
                return View("Crear", model);
            }

            // Suma si ya existe en carrito (mismo producto y mismo usuario/anónimo)
            var existente = await _context.CarritoCompra
                .FirstOrDefaultAsync(c => c.Activo
                                       && c.ID_Producto == model.ID_Producto
                                       && c.ID_Usuario == model.ID_Usuario);

            if (existente != null)
            {
                var nuevaCantidad = existente.Cantidad + model.Cantidad;
                if (nuevaCantidad > producto!.Stock)
                {
                    ModelState.AddModelError(nameof(model.Cantidad),
                        $"No se puede agregar. Ya tienes {existente.Cantidad} en el carrito. Stock disponible: {producto.Stock}");
                    await CargarProductosAsync(model.ID_Producto);
                    await CargarClientesAsync(model.ID_Usuario);
                    return View("Crear", model);
                }

                existente.Cantidad = nuevaCantidad;
                existente.PrecioUnitario = producto.Precio;
                existente.Subtotal = existente.Cantidad * existente.PrecioUnitario;
                existente.FechaAgregado = DateTime.Now;

                await _context.SaveChangesAsync();
                TempData["Success"] = "Cantidad actualizada en el carrito.";
                return RedirectToAction(nameof(Index));
            }

            model.PrecioUnitario = producto!.Precio;
            model.Subtotal = model.Cantidad * model.PrecioUnitario;
            model.FechaAgregado = DateTime.Now;
            model.Activo = true;

            _context.CarritoCompra.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Producto agregado al carrito.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var item = await _context.CarritoCompra
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.ID_Carrito == id);

            if (item == null) return NotFound();

            await CargarProductosAsync(item.ID_Producto);
            await CargarClientesAsync(item.ID_Usuario);
            return View("Editar", item);
        }

        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(CarritoCompra model)
        {
            var itemDb = await _context.CarritoCompra.FirstOrDefaultAsync(c => c.ID_Carrito == model.ID_Carrito);
            if (itemDb == null) return NotFound();

            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.ID_Producto == itemDb.ID_Producto && p.Activo);
            if (producto == null) ModelState.AddModelError("", "Producto inválido.");
            if (model.Cantidad <= 0) ModelState.AddModelError(nameof(model.Cantidad), "Cantidad inválida.");
            if (producto != null && model.Cantidad > producto.Stock)
                ModelState.AddModelError(nameof(model.Cantidad), $"Stock insuficiente. Disponible: {producto.Stock}");

            if (!ModelState.IsValid)
            {
                await CargarProductosAsync(itemDb.ID_Producto);
                await CargarClientesAsync(itemDb.ID_Usuario);
                itemDb.Cantidad = model.Cantidad;
                return View("Editar", itemDb);
            }

            itemDb.Cantidad = model.Cantidad;
            itemDb.PrecioUnitario = producto!.Precio;
            itemDb.Subtotal = itemDb.Cantidad * itemDb.PrecioUnitario;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Carrito actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var item = await _context.CarritoCompra
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(c => c.ID_Carrito == id);

            if (item == null) return NotFound();

            return View("Eliminar", item);
        }

        [HttpPost("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var item = await _context.CarritoCompra.FindAsync(id);
            if (item == null) return NotFound();

            item.Activo = false;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Producto eliminado del carrito.";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Limpiar todo el carrito
        // =========================
        [HttpPost("LimpiarCarrito")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LimpiarCarrito()
        {
            var items = await _context.CarritoCompra
                .Where(c => c.Activo)
                .ToListAsync();

            foreach (var item in items)
            {
                item.Activo = false;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Carrito limpiado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}