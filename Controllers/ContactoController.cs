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
    public class ContactoController : Controller
    {
         private readonly ILogger<ContactoController> _logger;

        private readonly ApplicationDbContext _context;

        public ContactoController(ILogger<ContactoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
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

        [HttpPost]
        public IActionResult Enviar(ContactoViewModel viewModel)
        {
            _logger.LogDebug("Ingreso a Contacto");

            var contacto = new Contacto
            {
                Nombre = viewModel.FormContacto.Nombre,
                Email = viewModel.FormContacto.Email,
                Telefono = viewModel.FormContacto.Telefono,
                Mensaje = viewModel.FormContacto.Mensaje,
            };

            _context.Add(contacto);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}