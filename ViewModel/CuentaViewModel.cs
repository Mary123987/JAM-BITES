using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAM_BITES.Models;

namespace JAM_BITES.ViewModel
{

    public class CuentaViewModel
    {
        public Cuenta? FormCuenta { get; set; }
        public IEnumerable<Cuenta>? ListCuenta { get; set; }
    }
}