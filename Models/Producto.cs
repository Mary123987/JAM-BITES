using System.ComponentModel.DataAnnotations.Schema;

namespace JAM_BITES.Models
{
    [Table("t_producto")]
    public class Producto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public string? ImageURL { get; set; }
        public string? FechaCreacion { get; set; }
        public Categoria? Categoria { get; set; }
    }
}