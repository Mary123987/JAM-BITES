using System.ComponentModel.DataAnnotations.Schema;

namespace JAM_BITES.Models
{
     [Table("t-cuenta")]
    public class CrearCuenta
    {
    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
