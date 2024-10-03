using System.ComponentModel.DataAnnotations.Schema;

namespace JAM_BITES.Models
{
    [Table("t-contacto")]
    public class Contacto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Mensaje { get; set; }
    }
}