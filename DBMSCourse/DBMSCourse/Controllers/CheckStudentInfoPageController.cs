using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DBMSCourse.Repositories;
using System.Runtime.Remoting.Contexts;

namespace DBMSCourse.Controllers
{
    public class CheckStudentInfoPageController : Controller
    {
        private readonly OverallReportRepository _overallReportRepository;

        public CheckStudentInfoPageController()
        {
            _overallReportRepository = new OverallReportRepository(new DBMSCourseContext());
        }

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
                if (string.IsNullOrEmpty(request.StudentInput) || string.IsNullOrEmpty(request.SectionId))
                {
                    return Json(new { success = false, message = "Both student input and section information are required." });
                }

                var payload = new
                {
                    studentInput = request.StudentInput,
                    sectionId = request.SectionId
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

        [HttpPost]
        public JsonResult UpdateStudentScore(int sectionId, float score)
        {
            try
            {
                var report = _overallReportRepository.GetOverallReportBySectionId(sectionId);
                if (report == null)
                {
                    return Json(new { success = false, message = "No report found for the given sectionId." });
                }
                report.KnowledgeCheckScore = Convert.ToDecimal(score);
                _overallReportRepository.UpdateOverallReportBySectionId(sectionId, report);

                return Json(new { success = true, message = "Score updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }

    public class EvaluationRequest
    {
        [JsonProperty("studentInput")]
        public string StudentInput { get; set; }

        [JsonProperty("sectionId")]
        public string SectionId { get; set; }
    }
}
