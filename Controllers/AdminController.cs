using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JAM_BITES.Models;
using JAM_BITES.Data;

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
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}