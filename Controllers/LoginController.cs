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
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;  
        }


        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Ingresar(Cuenta model)
        {

            _logger.LogInformation($"Username: {model.Email}");
            _logger.LogInformation($"Phone: {model.Password}");

            var user = _context.DataCuenta.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (user != null)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
            }
            return View("IniciarSesion");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}