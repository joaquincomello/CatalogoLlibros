using System.ComponentModel.DataAnnotations;

namespace CatalogoLlibros.Models
{
    public class Autor
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
    }
}