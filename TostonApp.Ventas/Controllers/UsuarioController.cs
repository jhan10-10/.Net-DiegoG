using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    [Route("Usuario")]
    [Route("Clientes")]
    public class UsuarioController : Controller
    {
        private readonly TostonDbContext _context;

        public UsuarioController(TostonDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: /Usuario
        // =========================
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios
                .Where(u => u.Activo)
                .OrderBy(u => u.Apellido)
                .ThenBy(u => u.Nombre)
                .AsNoTracking()
                .ToListAsync();

            return View("Index", usuarios);
        }

        // =========================
        // GET: /Usuario/Detalle/1
        // =========================
        [HttpGet("Detalle/{id:int}")]
        public async Task<IActionResult> Detalle(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Pedidos.Where(p => p.Activo))
                    .ThenInclude(p => p.Detalles)
                        .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(u => u.ID_Usuario == id);

            if (usuario == null) return NotFound();

            return View("Detalle", usuario);
        }

        // =========================
        // GET: /Usuario/Crear
        // =========================
        [HttpGet("Crear")]
        public IActionResult Crear()
        {
            return View("Crear", new Usuario());
        }

        // =========================
        // POST: /Usuario/Crear
        // =========================
        [HttpPost("Crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Usuario model)
        {
            // Validar cédula única
            var existeCedula = await _context.Usuarios
                .AnyAsync(u => u.Cedula == model.Cedula && u.Activo);

            if (existeCedula)
            {
                ModelState.AddModelError(nameof(model.Cedula),
                    "Ya existe un cliente registrado con esta cédula.");
            }

            // Validar correo único (si se proporciona)
            if (!string.IsNullOrWhiteSpace(model.Correo_Electronico))
            {
                var existeCorreo = await _context.Usuarios
                    .AnyAsync(u => u.Correo_Electronico == model.Correo_Electronico && u.Activo);

                if (existeCorreo)
                {
                    ModelState.AddModelError(nameof(model.Correo_Electronico),
                        "Este correo electrónico ya está registrado.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View("Crear", model);
            }

            model.Activo = true;
            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Cliente {model.Nombre} {model.Apellido} registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // GET: /Usuario/Editar/1
        // =========================
        [HttpGet("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            return View("Editar", usuario);
        }

        // =========================
        // POST: /Usuario/Editar
        // =========================
        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Usuario model)
        {
            var usuarioDb = await _context.Usuarios.FindAsync(model.ID_Usuario);
            if (usuarioDb == null) return NotFound();

            // Validar cédula única (excepto el actual)
            var existeCedula = await _context.Usuarios
                .AnyAsync(u => u.Cedula == model.Cedula
                            && u.ID_Usuario != model.ID_Usuario
                            && u.Activo);

            if (existeCedula)
            {
                ModelState.AddModelError(nameof(model.Cedula),
                    "Ya existe otro cliente con esta cédula.");
            }

            // Validar correo único (si se proporciona)
            if (!string.IsNullOrWhiteSpace(model.Correo_Electronico))
            {
                var existeCorreo = await _context.Usuarios
                    .AnyAsync(u => u.Correo_Electronico == model.Correo_Electronico
                                && u.ID_Usuario != model.ID_Usuario
                                && u.Activo);

                if (existeCorreo)
                {
                    ModelState.AddModelError(nameof(model.Correo_Electronico),
                        "Este correo ya está registrado para otro cliente.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View("Editar", model);
            }

            usuarioDb.Cedula = model.Cedula;
            usuarioDb.Nombre = model.Nombre;
            usuarioDb.Apellido = model.Apellido;
            usuarioDb.Correo_Electronico = model.Correo_Electronico;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Cliente actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // GET: /Usuario/Eliminar/1
        // =========================
        [HttpGet("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Pedidos.Where(p => p.Activo))
                .FirstOrDefaultAsync(u => u.ID_Usuario == id);

            if (usuario == null) return NotFound();

            return View("Eliminar", usuario);
        }

        // =========================
        // POST: /Usuario/EliminarConfirmado
        // =========================
        [HttpPost("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            // Soft delete
            usuario.Activo = false;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Cliente desactivado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // API: Buscar por cédula (AJAX)
        // =========================
        [HttpGet("BuscarPorCedula")]
        public async Task<IActionResult> BuscarPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return Json(new { success = false, message = "Cédula requerida" });

            var usuario = await _context.Usuarios
                .Where(u => u.Cedula == cedula && u.Activo)
                .Select(u => new
                {
                    u.ID_Usuario,
                    u.Cedula,
                    u.Nombre,
                    u.Apellido,
                    u.Correo_Electronico,
                    NombreCompleto = u.Nombre + " " + u.Apellido
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
                return Json(new { success = false, message = "Cliente no encontrado" });

            return Json(new { success = true, data = usuario });
        }
    }
}