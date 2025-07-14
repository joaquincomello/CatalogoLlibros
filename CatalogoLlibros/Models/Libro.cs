using System.ComponentModel.DataAnnotations;

namespace CatalogoLlibros.Models
{
    public class Libro
    {

        public int id { get; set; }

        [Required]
        public string titulo { get; set; }

        //[Range(1800, 2100)]
        [Required]
        public DateTime anioPublicacion { get; set; }

       // [Required]
        public Autor autor { get; set; }

        public string UrlImagen { get; set; }
         public int autorId { get; set; }   


    }
}
