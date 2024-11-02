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
using MLSentymentalAnalysis;


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

             MLModelTextClassification.ModelInput sampleData = new MLModelTextClassification.ModelInput()
            {
                Comentario = viewModel.FormContacto.Mensaje
            };
            
            MLModelTextClassification.ModelOutput output = MLModelTextClassification.Predict(sampleData);

                  //Console.WriteLine($"{output.Label}{output.PredictedLabel}");

            //output.Score.ToList().ForEach(score => Console.WriteLine(score));

            var sortedScoresWithLabel = MLModelTextClassification.PredictAllLabels(sampleData);
            var scoreKeyFirst = sortedScoresWithLabel.ToList()[0].Key;
            var scoreValueFirst = sortedScoresWithLabel.ToList()[0].Value;
            var scoreKeySecond = sortedScoresWithLabel.ToList()[1].Key;
            var scoreValueSecond = sortedScoresWithLabel.ToList()[1].Value;

            if(scoreKeyFirst == "1")
            {
                if(scoreValueFirst > 0.5)
                {
                    viewModel.FormContacto.Categoria = "Positivo";
                }
                else
                {
                    viewModel.FormContacto.Categoria = "Negativo";
                }
            }else{
                if(scoreValueFirst > 0.5)
                {
                    viewModel.FormContacto.Categoria = "Negativo";
                }
                else
                {
                    viewModel.FormContacto.Categoria = "Positivo";
                }
            }
            
            Console.WriteLine($"{scoreKeyFirst,-40}{scoreValueFirst,-20}");
            Console.WriteLine($"{scoreKeySecond,-40}{scoreValueSecond,-20}");


            /*foreach (var score in sortedScoresWithLabel)
            {
                Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
            }*/
            

            var contacto = new Contacto
            {
                Nombre = viewModel.FormContacto.Nombre,
                Email = viewModel.FormContacto.Email,
                Telefono = viewModel.FormContacto.Telefono,
                Mensaje = viewModel.FormContacto.Mensaje,
                Categoria = viewModel.FormContacto.Categoria
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