using Hulujan_Iulia_Petruta_proiectNouM.Models;
using Hulujan_Iulia_Petruta_proiectNouM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Hulujan_Iulia_Petruta_proiectNouM.Controllers
{
    public class StudentPredictionController : Controller
    {
        private readonly IStudentPredictionService _studentService;
        public StudentPredictionController(IStudentPredictionService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new StudentPredictionViewModel();
            return View( model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(StudentPredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var input = new StudentInput
            {
                 hours_Studied= model.hours_Studied,
                 previous_Scores= model.previous_Scores,
                 sleep_Hours= model.sleep_Hours,
                 sample_Question_Papers_Practiced= model.sample_Question_Papers_Practiced
            };
            // apelam Web API-ul

            var prediction = await _studentService.PredictStudentAsync(input);
            if (float.TryParse(prediction, out var result))
            {
                model.score = result;
            }
            else
            {
                ModelState.AddModelError("", "Predictie invalida.");
            }
            return View( model);

        }
    }
}
