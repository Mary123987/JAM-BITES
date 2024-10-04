using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JAM_BITES.Data;
using JAM_BITES.Models;
using JAM_BITES.ViewModel;

namespace JAM_BITES.Controllers
{

    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly ApplicationDbContext _context;

        public RegisterController(ILogger<RegisterController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
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

        [HttpPost]
        public IActionResult CrearCuenta(CuentaViewModel viewModel)
        {
            var cuenta = new Cuenta
            {
                Email = viewModel.FormCuenta.Email,
                Password = viewModel.FormCuenta.Password
            };

            _context.Add(cuenta);
            _context.SaveChanges();

            if (viewModel.FormCuenta == null)
            {
                _logger.LogError("FormCuenta es nulo");
                ViewBag.ErrorMessage = "Por favor, completa todos los campos.";
                return View("Index", viewModel); 
            }

            ViewBag.SuccessMessage = "Registro exitoso. Ahora puedes iniciar sesión.";
            return View("Index", viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}