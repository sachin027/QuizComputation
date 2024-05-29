using Newtonsoft.Json;
using QuizComputation.Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuizComputation.Common
{
    public class WebAPIHelper
    {
        public static string ApiUrl = "http://localhost:51143/api/";

        public static async Task<List<QuizModel>> CreatedQuizListForUser()
        {
            List<QuizModel> _QuizList = new List<QuizModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync($"AdminAPI/GetAllCreatedQuizListForAdmin");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    _QuizList = JsonConvert.DeserializeObject<List<QuizModel>>(responseData);
                }
            };
            return _QuizList;
        }

        public static async Task<QuizModel> CreateQuiz(QuizModel quizModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                string content = JsonConvert.SerializeObject(quizModel);
                var response = await client.PostAsync("AdminAPI/CreateQuiz", new StringContent(content, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<QuizModel>(responseData);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("The requested resource was not found (404). Please check the API endpoint.");
                }
                else
                {
                    throw new Exception("Failed to create quiz. Status code: " + response.StatusCode);
                }
            }
        }

        public static async Task<List<QuizModel>> GetQuizListForUser()
        {
            List<QuizModel> _QuizList = new List<QuizModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync($"UserAPI/GetQuizListForUser");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    _QuizList = JsonConvert.DeserializeObject<List<QuizModel>>(responseData);
                }
            };
            return _QuizList;
        }

        public static async Task DeleteQuiz(int QuizId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                await client.DeleteAsync($"UserAPI/DeleteQuiz?QuizId={QuizId}");
            }
        }
    }
}