using System.ComponentModel.DataAnnotations.Schema;

namespace JAM_BITES.Models
{
    [Table("t-usuario")]
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
    }
}