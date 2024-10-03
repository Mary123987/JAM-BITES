using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JAM_BITES.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Ingresar(string email, string password)
        {

            if (email == "admin@ejemplo.com" && password == "123456")
            {
                return RedirectToAction("Dashboard");  // Nombre del MENÚ
            }

            ViewBag.ErrorMessage = "Correo o contraseña incorrectos";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}