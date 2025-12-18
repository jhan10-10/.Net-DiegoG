using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    // CORRECTO: DevolucionesController (con 's') para carpeta Devoluciones
    public class DevolucionesController : Controller
    {
        private readonly TostonDbContext _context;

        public DevolucionesController(TostonDbContext context)
        {
            _context = context;
        }

        // ===================== INDEX =====================
        public IActionResult Index()
        {
            var lista = _context.Devoluciones
                .Include(d => d.Venta)
                .Where(d => d.Activo)
                .OrderByDescending(d => d.Fecha)
                .ToList();
            return View(lista);
        }

        // ===================== DETALLE =====================
        public IActionResult Detalle(int id)
        {
            var devolucion = _context.Devoluciones
                .Include(d => d.Venta)
                .FirstOrDefault(d => d.ID_devolucion == id);

            if (devolucion == null)
            {
                TempData["Error"] = "Devolución no encontrada";
                return RedirectToAction("Index");
            }

            return View(devolucion);
        }

        // ===================== CREAR (GET) =====================
        public IActionResult Crear()
        {
            try
            {
                // Cargar TODAS las ventas activas (no solo completadas)
                var ventas = _context.Ventas
                    .Where(v => v.Activo)
                    .OrderByDescending(v => v.Fecha_Venta)
                    .ToList();

                Console.WriteLine($"=== CREAR DEVOLUCIÓN ===");
                Console.WriteLine($"Total ventas encontradas: {ventas.Count}");

                ViewBag.Ventas = ventas;

                if (ventas.Count == 0)
                {
                    TempData["Warning"] = "No hay ventas disponibles. Debe crear una venta primero.";
                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERROR AL CARGAR CREAR ===");
                Console.WriteLine($"Error: {ex.Message}");

                TempData["Error"] = $"Error al cargar el formulario: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // ===================== CREAR (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Devolucion devolucion)
        {
            try
            {
                // Remover validación de navegación
                ModelState.Remove("Venta");

                // Validaciones adicionales
                if (devolucion.ID_venta <= 0)
                {
                    ModelState.AddModelError("ID_venta", "Debe seleccionar una venta");
                }

                if (devolucion.ID_Producto <= 0)
                {
                    ModelState.AddModelError("ID_Producto", "Debe seleccionar un producto");
                }

                if (devolucion.MontoDevolucion <= 0)
                {
                    ModelState.AddModelError("MontoDevolucion", "El monto debe ser mayor a cero");
                }

                // Debug: Mostrar errores en consola
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("=== ERRORES DE VALIDACIÓN ===");
                    foreach (var key in ModelState.Keys)
                    {
                        var errors = ModelState[key].Errors;
                        if (errors.Any())
                        {
                            Console.WriteLine($"Campo: {key}");
                            foreach (var error in errors)
                            {
                                Console.WriteLine($"  - {error.ErrorMessage}");
                            }
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    devolucion.Fecha = DateTime.Now;
                    devolucion.Activo = true;

                    // Si no se especifica estado, poner Pendiente por defecto
                    if (string.IsNullOrEmpty(devolucion.Estado))
                    {
                        devolucion.Estado = "Pendiente";
                    }

                    _context.Devoluciones.Add(devolucion);
                    _context.SaveChanges();

                    TempData["Success"] = "Devolución registrada exitosamente";
                    return RedirectToAction("Index");
                }

                // Si hay errores, recargar datos
                ViewBag.Ventas = _context.Ventas
                    .Where(v => v.Activo && v.Estado == "Completada")
                    .OrderByDescending(v => v.Fecha_Venta)
                    .ToList();

                // ViewBag.Productos = _context.Productos
                //     .Where(p => p.Activo)
                //     .ToList();

                return View(devolucion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERROR AL CREAR DEVOLUCIÓN ===");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");

                TempData["Error"] = $"Error al crear la devolución: {ex.Message}";

                ViewBag.Ventas = _context.Ventas
                    .Where(v => v.Activo && v.Estado == "Completada")
                    .ToList();

                // ViewBag.Productos = _context.Productos
                //     .Where(p => p.Activo)
                //     .ToList();

                return View(devolucion);
            }
        }

        // ===================== EDITAR (GET) =====================
        public IActionResult Editar(int id)
        {
            var devolucion = _context.Devoluciones.Find(id);

            if (devolucion == null)
            {
                TempData["Error"] = "Devolución no encontrada";
                return RedirectToAction("Index");
            }

            // Cargar ventas y productos
            ViewBag.Ventas = _context.Ventas
                .Where(v => v.Activo)
                .OrderByDescending(v => v.Fecha_Venta)
                .ToList();

            // ViewBag.Productos = _context.Productos
            //     .Where(p => p.Activo)
            //     .ToList();

            return View(devolucion);
        }

        // ===================== EDITAR (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Devolucion devolucion)
        {
            try
            {
                ModelState.Remove("Venta");

                if (ModelState.IsValid)
                {
                    _context.Devoluciones.Update(devolucion);
                    _context.SaveChanges();

                    TempData["Success"] = "Devolución actualizada exitosamente";
                    return RedirectToAction("Index");
                }

                ViewBag.Ventas = _context.Ventas
                    .Where(v => v.Activo)
                    .ToList();

                // ViewBag.Productos = _context.Productos
                //     .Where(p => p.Activo)
                //     .ToList();

                return View(devolucion);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al actualizar: {ex.Message}";

                ViewBag.Ventas = _context.Ventas.Where(v => v.Activo).ToList();
                // ViewBag.Productos = _context.Productos.Where(p => p.Activo).ToList();

                return View(devolucion);
            }
        }

        // ===================== ELIMINAR (GET) =====================
        public IActionResult Eliminar(int id)
        {
            var devolucion = _context.Devoluciones.Find(id);

            if (devolucion == null)
            {
                TempData["Error"] = "Devolución no encontrada";
                return RedirectToAction("Index");
            }

            return View(devolucion);
        }

        // ===================== ELIMINAR (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var devolucion = _context.Devoluciones.Find(id);

                if (devolucion == null)
                {
                    TempData["Error"] = "Devolución no encontrada";
                    return RedirectToAction("Index");
                }

                // Soft delete
                devolucion.Activo = false;
                _context.SaveChanges();

                TempData["Success"] = "Devolución eliminada exitosamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // ===================== TOGGLE ESTADO =====================
        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            try
            {
                var devolucion = _context.Devoluciones.Find(id);

                if (devolucion == null)
                {
                    return Json(new { success = false, message = "Devolución no encontrada" });
                }

                devolucion.Activo = !devolucion.Activo;
                _context.SaveChanges();

                return Json(new { success = true, activo = devolucion.Activo });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // ===================== APROBAR DEVOLUCIÓN =====================
        [HttpPost]
        public IActionResult AprobarDevolucion(int id)
        {
            try
            {
                var devolucion = _context.Devoluciones.Find(id);

                if (devolucion == null)
                {
                    return Json(new { success = false, message = "Devolución no encontrada" });
                }

                devolucion.Estado = "Aprobada";
                _context.SaveChanges();

                return Json(new { success = true, message = "Devolución aprobada exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // ===================== RECHAZAR DEVOLUCIÓN =====================
        [HttpPost]
        public IActionResult RechazarDevolucion(int id, string motivo)
        {
            try
            {
                var devolucion = _context.Devoluciones.Find(id);

                if (devolucion == null)
                {
                    return Json(new { success = false, message = "Devolución no encontrada" });
                }

                devolucion.Estado = "Rechazada";
                if (!string.IsNullOrEmpty(motivo))
                {
                    devolucion.Observaciones += $"\nMotivo rechazo: {motivo}";
                }
                _context.SaveChanges();

                return Json(new { success = true, message = "Devolución rechazada" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}