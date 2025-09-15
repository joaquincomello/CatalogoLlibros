using Microsoft.EntityFrameworkCore;

namespace CatalogoLlibros.Models
{
    public class CatalogoContext : DbContext
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Libro>()
                .HasOne(l => l.autor)
                .WithMany()
                .HasForeignKey(l => l.autorId);

            
            modelBuilder.Entity<Autor>().HasData(
                new Autor { id = 1, nombre = "George Orwell" },
                new Autor { id = 2, nombre = "Ray Bradbury" },
                new Autor { id = 3, nombre = "Aldous Huxley" }
            );

            modelBuilder.Entity<Libro>().HasData(
                new Libro { id = 1, titulo = "1984", anioPublicacion = new DateTime(1949, 1, 1), autorId = 1, UrlImagen = "https://m.media-amazon.com/images/I/61kjuGfZyML.jpg" },
                new Libro { id = 2, titulo = "Fahrenheit 451", anioPublicacion = new DateTime(1953, 1, 1), autorId = 2, UrlImagen = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg" },
                new Libro { id = 3, titulo = "Rebelión en la granja", anioPublicacion = new DateTime(1945, 1, 1), autorId = 1, UrlImagen = "https://images.cdn1.buscalibre.com/fit-in/360x360/68/e0/68e0aac2ed0bfe4c39e0cf16663a5918.jpg" }
            );
        }
    }
}