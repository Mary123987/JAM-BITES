using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JAM_BITES.Models
{
    [Table("t_order_detail")]
    public class DetallePedido
    {    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID {get; set;}

        public Producto? Producto {get; set;}
        public int Cantidad{get; set;}
        public Producto? Precio { get; set; }
        public Pedido? pedido {get; set;}        
    }
}