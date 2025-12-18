using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    [Route("Pedido")]
    public class PedidoController : Controller
    {
        private readonly TostonDbContext _context;
        public PedidoController(TostonDbContext context) => _context = context;

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
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.Fecha)
                .AsNoTracking()
                .ToListAsync();

            return View("Index", pedidos);
        }

        [HttpGet("Detalle/{id:int}")]
        public async Task<IActionResult> Detalle(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.ID_Pedido == id);

            if (pedido == null) return NotFound();
            return View("Detalle", pedido);
        }

        // =========================
        // GET: /Pedido/Crear
        // =========================
        [HttpGet("Crear")]
        public async Task<IActionResult> Crear()
        {
            await CargarClientesAsync();
            return View("Crear");
        }

        // =========================
        // ✅ Crear pedido desde carrito con cliente
        // =========================
        [HttpPost("CrearDesdeCarrito")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearDesdeCarrito(int? idUsuario, string? cedula, string? observaciones)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // Obtener carrito activo (puede filtrar por usuario si quieres)
                var carrito = await _context.CarritoCompra
                    .Include(c => c.Producto)
                    .Where(c => c.Activo)
                    .ToListAsync();

                if (carrito.Count == 0)
                {
                    TempData["Error"] = "El carrito está vacío.";
                    return RedirectToAction("Index", "Carrito");
                }

                // Validar cliente si se proporcionó
                Usuario? usuario = null;
                if (idUsuario.HasValue)
                {
                    usuario = await _context.Usuarios.FindAsync(idUsuario.Value);
                    if (usuario == null || !usuario.Activo)
                    {
                        TempData["Error"] = "Cliente no válido.";
                        return RedirectToAction("Index", "Carrito");
                    }
                }

                // Validar stock y calcular total
                decimal total = 0m;

                foreach (var c in carrito)
                {
                    if (c.Producto == null || !c.Producto.Activo)
                        throw new Exception("Hay productos inválidos en el carrito.");

                    if (c.Cantidad > c.Producto.Stock)
                        throw new Exception($"Stock insuficiente para {c.Producto.Nombre}. Disponible: {c.Producto.Stock}");
                }

                var pedido = new Pedido
                {
                    Fecha = DateTime.Now,
                    ID_Usuario = idUsuario,
                   
                    Cedula = usuario?.Cedula ?? cedula,
                    Observaciones = observaciones,
                    Estado = "Pendiente",
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync(); // para obtener ID_Pedido

                // Crear detalles
                foreach (var c in carrito)
                {
                    var precio = c.Producto!.Precio;
                    var subtotal = precio * c.Cantidad;
                    total += subtotal;

                    _context.PedidoDetalles.Add(new PedidoDetalle
                    {
                        ID_Pedido = pedido.ID_Pedido,
                        ID_Producto = c.ID_Producto,
                        Cantidad = c.Cantidad,
                        PrecioUnitario = precio,
                        Subtotal = subtotal
                    });

                    // Vaciar carrito (soft delete)
                    c.Activo = false;
                }

                pedido.Total = total;

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                TempData["Success"] = $"Pedido #{pedido.ID_Pedido} creado correctamente.";
                return RedirectToAction(nameof(Detalle), new { id = pedido.ID_Pedido });
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                TempData["Error"] = $"No se pudo crear el pedido: {ex.Message}";
                return RedirectToAction("Index", "Carrito");
            }
        }

        // =========================
        // GET: /Pedido/Editar/1
        // =========================
        [HttpGet("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.ID_Pedido == id);

            if (pedido == null) return NotFound();

            await CargarClientesAsync(pedido.ID_Usuario);
            return View("Editar", pedido);
        }

        // =========================
        // POST: /Pedido/Editar
        // =========================
        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Pedido model)
        {
            var pedidoDb = await _context.Pedidos.FindAsync(model.ID_Pedido);
            if (pedidoDb == null) return NotFound();

            // Validar cliente si se cambió
            if (model.ID_Usuario.HasValue)
            {
                var usuario = await _context.Usuarios.FindAsync(model.ID_Usuario.Value);
                if (usuario == null || !usuario.Activo)
                {
                    ModelState.AddModelError(nameof(model.ID_Usuario), "Cliente no válido.");
                }
            }

            if (!ModelState.IsValid)
            {
                await CargarClientesAsync(model.ID_Usuario);
                return View("Editar", model);
            }

            pedidoDb.ID_Usuario = model.ID_Usuario;
            pedidoDb.Cedula = model.Cedula;
            pedidoDb.Estado = model.Estado;
            pedidoDb.Observaciones = model.Observaciones;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Pedido actualizado correctamente.";
            return RedirectToAction(nameof(Detalle), new { id = model.ID_Pedido });
        }

        // =========================
        // GET: /Pedido/Eliminar/1
        // =========================
        [HttpGet("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.ID_Pedido == id);

            if (pedido == null) return NotFound();

            return View("Eliminar", pedido);
        }

        // =========================
        // POST: /Pedido/EliminarConfirmado
        // =========================
        [HttpPost("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            // Soft delete
            pedido.Activo = false;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Pedido cancelado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}