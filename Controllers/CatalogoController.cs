using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using JAM_BITES.Data;
using JAM_BITES.Models;

namespace JAM_BITES.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;

        public CatalogoController(ILogger<CatalogoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Método para mostrar todos los productos
        public IActionResult Index()
        {
            ViewBag.Categorias = HttpContext.Items["Categorias"];
            var catalogos = _context.DataProducto
                .Include(p => p.Categoria)
                .ToList();
            return View(catalogos);
        }

        // Método para mostrar productos por categoría
        public IActionResult PorCategoria(long categoriaId)
        {
            var productos = _context.DataProducto
                .Include(p => p.Categoria)
                .Where(p => p.Categoria.Id == categoriaId)
                .ToList();

            ViewData["CategoriaActual"] = _context.DataCategoria
                .FirstOrDefault(c => c.Id == categoriaId)?.Nombre;

            return View("Index", productos);
        }

        // Método para mostrar productos por rango de precio
        public IActionResult PorPrecio(decimal precioMin, decimal precioMax)
        {
            var productos = _context.DataProducto
                .Include(p => p.Categoria)
                .Where(p => p.Precio >= precioMin && p.Precio <= precioMax)
                .ToList();

            ViewData["RangoPrecio"] = $"Productos entre {precioMin} y {precioMax} soles";

            return View("Index", productos);
        }

        // Método para buscar productos
        public IActionResult Buscar(string termino)
        {
            var productos = _context.DataProducto
                .Include(p => p.Categoria)
                .Where(p => p.Nombre.Contains(termino) ||
                           p.Descripcion.Contains(termino))
                .ToList();

            ViewData["TerminoBusqueda"] = termino;

            return View("Index", productos);
        }

        // Método para mostrar el detalle de un producto
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.DataProducto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}