using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    [Route("Venta")]
    [Route("Ventas/Venta")]
    public class VentaController : Controller
    {
        private readonly TostonDbContext _context;

        public VentaController(TostonDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: /Venta
        // =========================
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Pedido)
                .OrderByDescending(v => v.Fecha_Venta)
                .AsNoTracking()
                .ToListAsync();

            return View("Index", ventas);
        }

        // =========================
        // GET: /Venta/Detalle/1
        // =========================
        [HttpGet("Detalle/{id:int}")]
        public async Task<IActionResult> Detalle(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Pedido)
                .FirstOrDefaultAsync(v => v.ID_venta == id);

            if (venta == null) return NotFound();

            return View("Detalle", venta);
        }

        // ✅ Aliases por si alguna vista usa plural o inglés
        [HttpGet("Detalles/{id:int}")]
        public Task<IActionResult> Detalles(int id) => Detalle(id);

        [HttpGet("Details/{id:int}")]
        public Task<IActionResult> Details(int id) => Detalle(id);

        // =========================
        // GET: /Venta/Eliminar/1
        // =========================
        [HttpGet("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Pedido)
                .FirstOrDefaultAsync(v => v.ID_venta == id);

            if (venta == null) return NotFound();

            return View("Eliminar", venta);
        }

        // =========================
        // POST: /Venta/EliminarConfirmado
        // =========================
        [HttpPost("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return NotFound();

            // Soft delete
            venta.Activo = false;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Venta eliminada (desactivada) correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // GET: /Venta/CrearDesdePedido/5
        // =========================
        [HttpGet("CrearDesdePedido/{id:int}")]
        public async Task<IActionResult> CrearDesdePedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.ID_Pedido == id);

            if (pedido == null) return NotFound();

            ViewBag.Pedido = pedido;
            return View("CrearDesdePedido");
        }

        // =========================
        // POST: /Venta/GenerarDesdePedido/5
        // =========================
        [HttpPost("GenerarDesdePedido/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerarDesdePedido(int id, string metodoPago)
        {
            if (string.IsNullOrWhiteSpace(metodoPago))
            {
                TempData["Error"] = "Debe seleccionar un método de pago.";
                return RedirectToAction(nameof(CrearDesdePedido), new { id });
            }

            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.Detalles)
                        .ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(p => p.ID_Pedido == id);

                if (pedido == null) return NotFound();
                if (pedido.Detalles == null || pedido.Detalles.Count == 0)
                    throw new Exception("El pedido no tiene productos.");

                // Recalcular + descontar stock
                decimal total = 0m;

                foreach (var det in pedido.Detalles)
                {
                    if (det.Producto == null || !det.Producto.Activo)
                        throw new Exception($"Producto inválido (ID_Producto={det.ID_Producto}).");

                    if (det.Cantidad > det.Producto.Stock)
                        throw new Exception($"Stock insuficiente para {det.Producto.Nombre}. Disponible: {det.Producto.Stock}");

                    det.PrecioUnitario = det.Producto.Precio;
                    det.Subtotal = det.Cantidad * det.PrecioUnitario;
                    total += det.Subtotal;

                    det.Producto.Stock -= det.Cantidad;
                }

                pedido.Total = total;
                pedido.Estado = "Completado";

                var venta = new Venta
                {
                    ID_Pedido = pedido.ID_Pedido,
                    Fecha_Venta = DateTime.Now,
                    Total = total,
                    MetodoPago = metodoPago,
                    Estado = "Completada",
                    Activo = true,
                    Observaciones = pedido.Observaciones
                };

                _context.Ventas.Add(venta);

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                TempData["Success"] = "Venta generada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                TempData["Error"] = $"No se pudo generar la venta: {ex.Message}";
                return RedirectToAction(nameof(CrearDesdePedido), new { id });
            }
        }
    }
}
