using System.Threading.Tasks;
using Hulujan_Iulia_Petruta_proiectNouM.Models;

namespace Hulujan_Iulia_Petruta_proiectNouM.Services
{
    public interface IStudentPredictionService
    {
        Task<string> PredictStudentAsync(StudentInput input);
    }
}
