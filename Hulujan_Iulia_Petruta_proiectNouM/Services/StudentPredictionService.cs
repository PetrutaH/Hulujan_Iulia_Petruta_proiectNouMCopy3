using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Hulujan_Iulia_Petruta_proiectNouM.Models;


namespace Hulujan_Iulia_Petruta_proiectNouM.Services
{
    public class StudentPredictionService : IStudentPredictionService
    {
        private readonly HttpClient _httpClient;
        public StudentPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> PredictStudentAsync(StudentInput input)
        {
            // POST 
            var response = await _httpClient.PostAsJsonAsync("https://localhost:50737/predict", input);
            response.EnsureSuccessStatusCode();
            
            var results = await response.Content.ReadFromJsonAsync<StudentApiResponse>();
            return results?.score.ToString();
        }

        private class StudentApiResponse
        {
            public float score { get; set; }
        }
    }
}
