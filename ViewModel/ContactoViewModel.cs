using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAM_BITES.Models;

namespace JAM_BITES.ViewModel
{

    public class ContactoViewModel
    {
        public Contacto? FormContacto { get; set; }
        public IEnumerable<Contacto>? ListContacto { get; set; }
    }
}