using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DBMSCourse.Controllers
{
    public class QAPageController : Controller
    {
        private readonly HttpClient _httpClient;

        public QAPageController()
        {
            // HttpClient, API çağrıları için kullanılır.
            _httpClient = new HttpClient();
        }

        // GET: QAPage
        public ActionResult QAPage(int? sectionId)
        {
            ViewBag.SectionId = sectionId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AskQuestion(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                return Json(new { success = false, message = "Question cannot be empty." });
            }

            var apiUrl = "http://localhost:5000/api/ask"; // Python API'nizin adresini buraya yazın.

            try
            {
                // Soru verisini JSON formatına dönüştür.
                var payload = JsonConvert.SerializeObject(new { question = question });
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                // Python API'sine POST isteği gönder.
                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // API cevabını işle.
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    return Json(new { success = true, answer = result?.answer });
                }
                else
                {
                    return Json(new { success = false, message = "API error: " + response.ReasonPhrase });
                }
            }
            catch
            {
                return Json(new { success = false, message = "Failed to connect to the API." });
            }
        }
    }
}
