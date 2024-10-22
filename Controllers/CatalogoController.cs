using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JAM_BITES.Models;

namespace JAM_BITES.Controllers;

public class CatalogoController : Controller
{
    private readonly ILogger<CatalogoController> _logger;

    public CatalogoController(ILogger<CatalogoController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
