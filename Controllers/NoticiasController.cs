using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JAM_BITES.Services;
using JAM_BITES.Models;

namespace JAM_BITES.Controllers
{
    [Route("[controller]")]
    public class NoticiasController : Controller
    {
        private readonly IServicioNoticias _servicioNoticias;

        public NoticiasController(IServicioNoticias servicioNoticias)
        {
            _servicioNoticias = servicioNoticias;
        }

        [HttpGet]
        public async Task<IActionResult> GetNoticias()
        {
            var noticias = await _servicioNoticias.ObtenerNoticiasAsync();
            if (noticias == null || noticias.Items.Count == 0)
            {
                return View("NoNoticias"); // Una vista que muestre un mensaje de no hay noticias.
            }
            return View(noticias); // Retorna la vista con el modelo de noticias
        }
    }
}
