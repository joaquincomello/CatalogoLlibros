using CatalogoLlibros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoMVC;

namespace ProyectoMVC.Controllers
{
    public class LibroController : Controller
    {
        
        private static List<Autor> autores = new List<Autor>
        {
            new Autor { id = 1, nombre = "George Orwell" },
            new Autor { id = 2, nombre = "Ray Bradbury" },
            new Autor { id = 3, nombre = "Aldous Huxley" }
        };

        
        private static List<Libro> libros = new List<Libro>
        {
            new Libro { id = 1, titulo = "1984", anioPublicacion = new DateTime(1949), autor = autores[0], autorId = 1, UrlImagen = "https://m.media-amazon.com/images/I/61kjuGfZyML.jpg" },
            new Libro { id = 2, titulo = "Fahrenheit 451", anioPublicacion =new DateTime (1953), autor = autores[1], autorId = 2, UrlImagen = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg" },
            new Libro { id = 3, titulo = "Rebelión en la granja", anioPublicacion =new DateTime (1945), autor = autores[0], autorId = 1, UrlImagen = "https://images.cdn1.buscalibre.com/fit-in/360x360/68/e0/68e0aac2ed0bfe4c39e0cf16663a5918.jpg" }
        };

        
        public IActionResult Index()
        {

            return View(libros);
        }
       
        public IActionResult FiltrarPorAutor()
        {
            var librosGeorgeOrwell = libros.Where(l => l.autor.nombre == "George Orwell").ToList();
            return View("Index", librosGeorgeOrwell);
        }


        public IActionResult Detalle(int id)
        {
            var libro = libros.FirstOrDefault(l => l.id == id);

            if (libro == null)
            {
                Console.WriteLine($"❌ No se encontró el libro con ID {id}");
                return RedirectToAction("Index");
            }

            Console.WriteLine($"✅ Mostrando detalles del libro {libro.titulo}");
            return View(libro);
        }


        public IActionResult Crear()
        {
            ViewBag.Autores = new SelectList(autores, "id", "nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Libro libro)
        {
            ModelState.Remove("autor");
            ModelState.Remove("UrlImagen");

            if (!ModelState.IsValid)
            {
                ViewBag.Autores = new SelectList(autores, "id", "nombre");
                return View(libro);
            }

            var autorSeleccionado = autores.FirstOrDefault(a => a.id == libro.autorId);
            if (autorSeleccionado == null)
            {
                ModelState.AddModelError("autorId", "El autor seleccionado no es válido.");
                ViewBag.Autores = new SelectList(autores, "id", "nombre");
                return View(libro);
            }

            libro.autor = autorSeleccionado;
            libro.id = libros.Any() ? libros.Max(l => l.id) + 1 : 1;
            libros.Add(libro);

            return RedirectToAction("Detalle", new { id = libro.id });
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            var libro = libros.FirstOrDefault(l => l.id == id);
            if (libro == null)
            {
                return NotFound();
            }
            libro.autorId = libro.autor?.id?? 0; // Asignar el ID del autor para el formulario
            ViewBag.Autores = autores;
            return View(libro);
        }


        [HttpPost]
        public IActionResult Editar(Libro libro)

        {
            ModelState.Remove("autor");
            ModelState.Remove("urlImagen");
            if (!ModelState.IsValid)
            {
                ViewBag.Autores = autores;
                return View(libro);
            }

            var libroExistente = libros.FirstOrDefault(l => l.id == libro.id);
            if (libroExistente == null)
            {
                return NotFound();
            }

            // Actualizar datos
            libroExistente.titulo = libro.titulo;
            libroExistente.anioPublicacion = libro.anioPublicacion;

            var autorSeleccionado = autores.FirstOrDefault(a => a.id == libro.autorId);
            if (autorSeleccionado != null)
            {
                libroExistente.autor = autorSeleccionado;
            }

            //libroExistente.UrlImagen = libro.UrlImagen;

            TempData["Mensaje"] = "Libro editado correctamente";

            return RedirectToAction("Detalle", new { id = libro.id });
        }
    }

}






