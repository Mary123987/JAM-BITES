using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JAM_BITES.Models;
using JAM_BITES.ViewModel;
using JAM_BITES.Data;
using Microsoft.EntityFrameworkCore;


namespace JAM_BITES.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Ingresar(Usuario model)
        {

            _logger.LogInformation($"Username: {model.User}");
            _logger.LogInformation($"Phone: {model.Password}");

            var user = _context.DataUsuario.FirstOrDefault(x => x.User == model.User && x.Password == model.Password);
            if (user != null)
            {
                return RedirectToAction("Panel", "Admin");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
            }
            return View("Index");
        }

        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult ListCuenta()
        {
            var miscuenta = from o in _context.DataCuenta select o;
            _logger.LogDebug("cuenta {miscuenta}", miscuenta);
            var viewModel = new CuentaViewModel
            {
                FormCuenta = new Cuenta(),
                ListCuenta = miscuenta
            };
            _logger.LogDebug("viewModel {viewModel}", viewModel);

            return View(viewModel);
        }

        public IActionResult ListContacto()
        {
            var miscontacto = from o in _context.DataContacto select o;
            _logger.LogDebug("contacto {miscontacto}", miscontacto);
            var viewModel = new ContactoViewModel
            {
                FormContacto = new Contacto(),
                ListContacto = miscontacto
            };
            _logger.LogDebug("viewModel {viewModel}", viewModel);

            return View(viewModel);
        }

        public IActionResult ListMenu()
        {
            var miscategoria = from o in _context.DataCategoria select o;

            var misproducto = from o in _context.DataProducto select o;

            var viewModel = new CategoriaProductoViewModel
            {
                ListCategoria = miscategoria,
                ListProducto = misproducto,
                FormProducto = new Producto(),// Inicializamos el producto para el formulario
            };

            _logger.LogDebug("viewModel {viewModel}", viewModel);

            return View(viewModel);
        }

        public IActionResult FormProducto()
        {
            return View();
        }
        public IActionResult CrearProducto(CategoriaProductoViewModel viewModel)
        {
            _logger.LogDebug("Ingreso a Crear Producto");

            var producto = new Producto
            {
                Nombre = viewModel.FormProducto.Nombre,
                Descripcion = viewModel.FormProducto.Descripcion,
                Precio = viewModel.FormProducto.Precio,
                ImageURL = viewModel.FormProducto.ImageURL,
                Categoria = viewModel.FormProducto.Categoria,
            };

            _context.Add(producto);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListMenu));
        }

        public IActionResult Redes()
        {
            return View();
        }

        // Lista todos los pedidos
        public async Task<IActionResult> ListPedido()
        {
            var pedidos = await _context.DataPedido
                .Include(p => p.pago)
                .OrderByDescending(p => p.pago.NombreTarjeta)
                .OrderByDescending(p => p.pago.PaymentDate)
                .ToListAsync();

            return View(pedidos);
        }

        // Actualizar el estado de un pedido
        [HttpPost]
        public async Task<IActionResult> ActualizarEstado(int id, string nuevoEstado)
        {
            var pedido = await _context.DataPedido.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Status = nuevoEstado;
            _context.Update(pedido);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Estado del pedido actualizado correctamente";
            return RedirectToAction(nameof(ListPedido));
        }

        // Filtrar pedidos por estado
        public async Task<IActionResult> FiltrarPedidos(string estado)
        {
            var pedidos = await _context.DataPedido
                .Include(p => p.pago)
                .Where(p => string.IsNullOrEmpty(estado) || p.Status == estado)
                .OrderByDescending(p => p.pago.PaymentDate)
                .ToListAsync();

            return View("ListPedido", pedidos);
        }

        // Buscar pedidos por fecha
        public async Task<IActionResult> BuscarPorFecha(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var query = _context.DataPedido
                .Include(p => p.pago)
                .AsQueryable();

            if (fechaInicio.HasValue)
            {
                query = query.Where(p => p.pago.PaymentDate >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                query = query.Where(p => p.pago.PaymentDate <= fechaFin.Value);
            }

            var pedidos = await query
                .OrderByDescending(p => p.pago.PaymentDate)
                .ToListAsync();

            return View("ListPedido", pedidos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}