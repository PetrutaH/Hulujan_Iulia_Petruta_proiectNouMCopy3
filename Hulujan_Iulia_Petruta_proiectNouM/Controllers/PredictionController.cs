using Hulujan_Iulia_Petruta_proiectNouM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using static Hulujan_Iulia_Petruta_proiectNouM.StudentModel;

namespace Hulujan_Iulia_Petruta_proiectNouM.Controllers
{
    public class PredictionController : Controller
    {
        public IActionResult StudentPerformance(ModelInput input)
        {
            MLContext mlContext = new MLContext();

            ITransformer mlModel = mlContext.Model.Load(@"..\Hulujan_Iulia_Petruta_proiectNouM\StudentModel.mlnet", out var modelInputSchema);

            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            ModelOutput result = predEngine.Predict(input);

            ViewBag.PerformanceIndex = result.Score; 

            return View(input);
        }

        // Metoda GET 
        [HttpGet]
        public IActionResult StudentPerformance()
        {
            return View(new ModelInput());
        }
    }
        //public IActionResult Index()
        //{
        //    return View();
        //}
}

