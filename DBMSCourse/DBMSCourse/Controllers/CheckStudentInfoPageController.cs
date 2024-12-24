using System.Linq;
using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using DBMSCourse.Repositories;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DBMSCourse.Controllers
{
    public class CheckStudentInfoPageController : Controller
    {

        // GET: CheckStudentInfoPage
        public ActionResult CheckStudentInfoPage(int? sectionId)
        {
            ViewBag.SectionId = sectionId;
            return View();
        }

        // POST: GetOverallScore
        [HttpPost]
        public async Task<JsonResult> GetOverallScore(EvaluationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.StudentInput) || string.IsNullOrEmpty(request.SectionInfo))
                {
                    return Json(new { success = false, message = "Both student input and section information are required." });
                }

                var payload = new
                {
                    studentInput = request.StudentInput,
                    sectionInfo = request.SectionInfo
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://127.0.0.1:5000/api/similarityScore");
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    var respons = await client.PostAsync("", content);

                    if (!respons.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, message = "Failed to communicate with Python API." });
                    }

                    var result = await respons.Content.ReadAsStringAsync();
                    var apiRespose = JsonConvert.DeserializeObject<dynamic>(result);

                    string score = apiRespose.similarityScore;
                    bool success = apiRespose.success;
                    string message = apiRespose.message;

                    if (success)
                    {
                        return Json(new { success, score });
                    }
                    else
                    {
                        return Json(new { success, message });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

    }

    public class EvaluationRequest
    {
        [JsonProperty("studentInput")]
        public string StudentInput { get; set; }

        [JsonProperty("sectionInfo")]
        public string SectionInfo { get; set; }
    }
}
