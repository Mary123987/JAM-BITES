using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JAM_BITES.Controllers
{

    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

         [HttpPost]
        public IActionResult CrearCuenta(string email, string password)
        {
            // Aquí deberías implementar la lógica para registrar al usuario en tu base de datos
            // Por ejemplo: comprobar si el correo ya existe y luego guardarlo

            // Simulación de un registro exitoso
            if (email != null && password != null)
            {
                // Guardar en la base de datos (pseudocódigo)
                // userRepository.Add(new User { Email = email, Password = password });

                // Redirigir a la página de inicio de sesión con un mensaje de éxito
                ViewBag.SuccessMessage = "Registro exitoso. Ahora puedes iniciar sesión.";
                return RedirectToAction("Index", "IniciarSesion");
            }

           
            ViewBag.ErrorMessage = "Por favor, completa todos los campos.";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}