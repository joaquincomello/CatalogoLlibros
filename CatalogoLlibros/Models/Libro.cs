using System.ComponentModel.DataAnnotations;

namespace CatalogoLlibros.Models
{
    public class Libro
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string titulo { get; set; }

        public DateTime anioPublicacion { get; set; }

        public Autor autor { get; set; }

        public string? UrlImagen { get; set; }

        [Required]
        public int autorId { get; set; }
    }
}