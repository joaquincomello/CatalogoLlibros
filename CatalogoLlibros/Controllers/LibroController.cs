using CatalogoLlibros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProyectoMVC.Controllers
{
    public class LibroController : Controller
    {
        private readonly CatalogoContext _context;

        public LibroController(CatalogoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros.Include(l => l.autor).ToListAsync();
            return View(libros);
        }

        public async Task<IActionResult> FiltrarPorAutor()
        {
            var librosGeorgeOrwell = await _context.Libros.Include(l => l.autor)
                                                         .Where(l => l.autor.nombre == "George Orwell")
                                                         .ToListAsync();
            return View("Index", librosGeorgeOrwell);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var libro = await _context.Libros.Include(l => l.autor)
                                             .FirstOrDefaultAsync(l => l.id == id);
            if (libro == null)
            {
                return RedirectToAction("Index");
            }
            return View(libro);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Autores = new SelectList(await _context.Autores.ToListAsync(), "id", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Libro libro)
        {
            ModelState.Remove("autor");
            ModelState.Remove("UrlImagen");

            if (!ModelState.IsValid)
            {
                ViewBag.Autores = new SelectList(await _context.Autores.ToListAsync(), "id", "nombre");
                return View(libro);
            }

            var autorSeleccionado = await _context.Autores.FirstOrDefaultAsync(a => a.id == libro.autorId);
            if (autorSeleccionado == null)
            {
                ModelState.AddModelError("autorId", "El autor seleccionado no es válido.");
                ViewBag.Autores = new SelectList(await _context.Autores.ToListAsync(), "id", "nombre");
                return View(libro);
            }

            libro.autor = autorSeleccionado;
            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Libro creado correctamente.";
            return RedirectToAction("Detalle", new { id = libro.id });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var libro = await _context.Libros.Include(l => l.autor)
                                             .FirstOrDefaultAsync(l => l.id == id);
            if (libro == null)
            {
                return NotFound();
            }

            
            ViewBag.Autores = new SelectList(await _context.Autores.ToListAsync(), "id", "nombre");
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Libro libro)
        {
            ModelState.Remove("autor");
            ModelState.Remove("UrlImagen");

            if (!ModelState.IsValid)
            {
                
                ViewBag.Autores = new SelectList(await _context.Autores.ToListAsync(), "id", "nombre");
                return View(libro);
            }

            var libroExistente = await _context.Libros.FirstOrDefaultAsync(l => l.id == libro.id);
            if (libroExistente == null)
            {
                return NotFound();
            }

            var autorSeleccionado = await _context.Autores.FirstOrDefaultAsync(a => a.id == libro.autorId);
            if (autorSeleccionado == null)
            {
                ModelState.AddModelError("autorId", "El autor seleccionado no es válido.");
                ViewBag.Autores = new SelectList(await _context.Autores.ToListAsync(), "id", "nombre");
                return View(libro);
            }

            libroExistente.titulo = libro.titulo;
            libroExistente.anioPublicacion = libro.anioPublicacion;
            libroExistente.UrlImagen = libro.UrlImagen;
            libroExistente.autor = autorSeleccionado;

            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Libro editado correctamente";

            return RedirectToAction("Detalle", new { id = libro.id });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var libro = await _context.Libros.Include(l => l.autor)
                                             .FirstOrDefaultAsync(l => l.id == id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.id == id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Libro eliminado correctamente.";
            return RedirectToAction("Index");
        }
    }
}