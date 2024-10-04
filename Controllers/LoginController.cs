using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JAM_BITES.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Ingresar(string email, string password)
        {
            if (email == "admin@ejemplo.com" && password == "123456")
            {

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Correo o contraseña incorrectos";
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}