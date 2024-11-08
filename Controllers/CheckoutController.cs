using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using JAM_BITES.Data;
using JAM_BITES.Models;
using JAM_BITES.Helper;
using Microsoft.EntityFrameworkCore;

namespace JAM_BITES.Controllers
{
    public class CheckoutController : Controller
    {

        private readonly ILogger<CheckoutController> _logger;
        private readonly ApplicationDbContext _context;

        public CheckoutController(ILogger<CheckoutController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Recojo()
        {
            return View();
        }
        public IActionResult Confirmacion()
        {
            return View();
        }
        public IActionResult Create()
        {
            var carrito = Helper.SessionExtensions.Get<List<Carrito>>(HttpContext.Session, "carritoSesion");
            decimal total = 0;
            if (carrito != null)
            {
                foreach (var item in carrito)
                {
                    total += item.Producto.Precio * item.Cantidad;
                }
            }
            Pago pago = new Pago();
            pago.MontoTotal = total;
            return View(pago);
        }

        /*public IActionResult Create(Decimal monto)
        {
            Pago pago = new Pago();
            pago.MontoTotal = monto;
            return View(pago);
        }*/

        [HttpPost]
        public IActionResult Pagar(Pago pago)
        {
            pago.PaymentDate = DateTime.UtcNow;
            _context.Add(pago);

            var itemCarrito = from o in _context.DataCarrito select o;
            itemCarrito = itemCarrito.
                Include(p => p.Producto).
                Where(s => s.Status.Equals("PENDIENTE"));

            Pedido pedido = new Pedido();
            pedido.Total = pago.MontoTotal;
            pedido.pago = pago;
            pedido.Status = "PENDIENTE";
            _context.Add(pedido);


            List<DetallePedido> itemsPedido = new List<DetallePedido>();
            foreach (var item in itemCarrito.ToList())
            {
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.pedido = pedido;
                detallePedido.Precio = item.Precio;
                detallePedido.Producto = item.Producto;
                detallePedido.Cantidad = item.Cantidad;
                itemsPedido.Add(detallePedido);
            }

            _context.AddRange(itemsPedido);

            foreach (Carrito c in itemCarrito.ToList())
            {
                c.Status = "PROCESADO";
            }
            _context.UpdateRange(itemCarrito);

            _context.SaveChanges();

            TempData["Message"] = "El pago se ha registrado";
            return View("Confirmacion");
        }
    }
}