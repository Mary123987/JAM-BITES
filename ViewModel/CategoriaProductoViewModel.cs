using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAM_BITES.Models;

namespace JAM_BITES.ViewModel
{
    public class CategoriaProductoViewModel
    {
        public IEnumerable<Categoria>? ListCategoria { get; set; }

        public Producto? FormProducto { get; set; }
        public IEnumerable<Producto>? ListProducto { get; set; }
    }
}