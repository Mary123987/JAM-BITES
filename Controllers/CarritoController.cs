using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using JAM_BITES.Data;
using JAM_BITES.Models;
using JAM_BITES.Helper;

namespace JAM_BITES.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ILogger<CarritoController> _logger;
        private readonly ApplicationDbContext _context;

        public CarritoController(ILogger<CarritoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Carrito> carrito = Helper.SessionExtensions.Get<List<Carrito>>(HttpContext.Session, "carritoSesion");
            if (carrito == null)
            {
                carrito = new List<Carrito>();
            }
            // Calcular el total del carrito
            decimal total = carrito.Sum(item => item.Producto.Precio * item.Cantidad);

            ViewData["Total"] = total;
            return View(carrito);
        }

        public async Task<IActionResult> Add(long? id)
        {
            //obtengo el carrito de memoria
            List<Carrito> carrito = Helper.SessionExtensions.Get<List<Carrito>>(HttpContext.Session, "carritoSesion");
            if (carrito == null)
            {
                carrito = new List<Carrito>();
            }

            //obtengo los datos del producto
            var producto = await _context.DataProducto.FindAsync(id);
            Carrito itemCarrito = new Carrito();
            itemCarrito.Producto = producto;
            itemCarrito.Cantidad = 1;

            carrito.Add(itemCarrito);

            //seteo el carrito en memoria

            Helper.SessionExtensions.Set<List<Carrito>>(HttpContext.Session, "carritoSesion", carrito);
            TempData["Message"] = "Se Agrego al carrito";
            _logger.LogInformation("Se agrego un producto al carrito");
            return RedirectToAction("Index", "Carrito");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(long? id, int cantidad)
        {
            // Obtengo el carrito de la sesión
            List<Carrito> carrito = Helper.SessionExtensions.Get<List<Carrito>>(HttpContext.Session, "carritoSesion");
            if (carrito != null && id.HasValue)
            {
                // Encuentra el producto en el carrito
                var item = carrito.FirstOrDefault(c => c.Producto.Id == id);
                if (item != null)
                {
                    // Actualiza la cantidad del producto
                    item.Cantidad = cantidad > 0 ? cantidad : 1; // Asegura que la cantidad no sea menor a 1
                }

                // Actualiza el carrito en la sesión
                Helper.SessionExtensions.Set<List<Carrito>>(HttpContext.Session, "carritoSesion", carrito);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(long? id)
        {
            // Obtiene el carrito de la sesión
            List<Carrito> carrito = Helper.SessionExtensions.Get<List<Carrito>>(HttpContext.Session, "carritoSesion");
            if (carrito != null && id.HasValue)
            {
                // Busca el producto en el carrito y lo elimina
                var item = carrito.FirstOrDefault(c => c.Producto.Id == id);
                if (item != null)
                {
                    carrito.Remove(item);
                }

                // Actualiza el carrito en la sesión
                Helper.SessionExtensions.Set<List<Carrito>>(HttpContext.Session, "carritoSesion", carrito);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}