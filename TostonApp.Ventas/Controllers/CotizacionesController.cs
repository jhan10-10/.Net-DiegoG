using Microsoft.AspNetCore.Mvc;
using TostonApp.Ventas.Models;

namespace TostonApp.Ventas.Controllers
{
    [Route("Cotizacion")]
    public class CotizacionController : Controller
    {
        private readonly TostonDbContext _context;

        public CotizacionController(TostonDbContext context)
        {
            _context = context;
        }

        // GET: /Cotizacion
        [HttpGet("")]
        public IActionResult Index()
        {
            var cotizaciones = _context.Cotizaciones.ToList();
            return View("Index", cotizaciones);
        }

        // GET: /Cotizacion/Detalles/1
        [HttpGet("Detalles/{id}")]
        public IActionResult Detalles(int id)
        {
            var model = _context.Cotizaciones.Find(id);
            if (model == null) return NotFound();

            return View("Detalles", model);
        }

        // GET: /Cotizacion/Crear
        [HttpGet("Crear")]
        public IActionResult Crear()
        {
            return View("Crear");
        }

        // POST: /Cotizacion/Crear
        [HttpPost("Crear")]
        public IActionResult Crear(Cotizacion model)
        {
            if (!ModelState.IsValid)
            {
                return View("Crear", model);
            }

            _context.Cotizaciones.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: /Cotizacion/Editar/1
        [HttpGet("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            var model = _context.Cotizaciones.Find(id);
            if (model == null) return NotFound();

            return View("Editar", model);
        }

        // POST: /Cotizacion/Editar
        [HttpPost("Editar/{id}")]
        public IActionResult Editar(Cotizacion model)
        {
            if (!ModelState.IsValid)
            {
                return View("Editar", model);
            }

            _context.Cotizaciones.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: /Cotizacion/Eliminar/1
        [HttpGet("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var model = _context.Cotizaciones.Find(id);
            if (model == null) return NotFound();

            return View("Eliminar", model);
        }

        // POST: /Cotizacion/Eliminar
        [HttpPost("Eliminar/{id}")]
        public IActionResult EliminarPost(int id)
        {
            var model = _context.Cotizaciones.Find(id);
            if (model == null) return NotFound();

            _context.Cotizaciones.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
