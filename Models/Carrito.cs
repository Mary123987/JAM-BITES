using System.ComponentModel.DataAnnotations.Schema;

namespace JAM_BITES.Models
{
    [Table("t_carrito")]
    public class Carrito
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        public Producto? Precio { get; set; }
        public string Status { get; set; } = "PENDIENTE";
    }
}