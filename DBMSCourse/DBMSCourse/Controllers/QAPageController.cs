using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DBMSCourse.Controllers
{
    public class QAPageController : Controller
    {
        // GET: QAPage
        public ActionResult QAPage(int? sectionId)
        {
            ViewBag.SectionId = sectionId;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SubmitQuestion(int sectionId, string question)
        {
            try
            {
                // Section bilgilerini veritabanından çek
                var sectionRepo = new DBMSCourse.Repositories.SectionRepository(new DBMSCourseDBContext());
                var section = sectionRepo.GetSectionById(sectionId);

                if (section == null)
                {
                    return Json(new { success = false, message = "Section not found." });
                }

                // Python API'sine gönderilecek JSON oluştur
                var payload = new
                {
                    question = question,
                    sectionInfo = section.DetailedInfo
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://127.0.0.1:5000/api/answer"); // Python API URL
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Python API'ye POST isteği gönder
                    var response = await client.PostAsync("", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, message = "Failed to communicate with Python API." });
                    }

                    // Python API'den dönen cevabı al
                    var result = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<dynamic>(result);

                    if ((bool)apiResponse.success)
                    {
                        return Json(new { success = true, answer = apiResponse.answer });
                    }
                    else
                    {
                        return Json(new { success = false, message = apiResponse.message.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
