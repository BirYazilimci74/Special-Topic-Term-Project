using System.Linq;
using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using DBMSCourse.Repositories;

namespace DBMSCourse.Controllers
{
    public class CheckStudentInfoPageController : Controller
    {
        private readonly SectionRepository _repository = new SectionRepository();

        // GET: CheckStudentInfoPage
        public ActionResult CheckStudentInfoPage()
        {
            
            return View();
        }

        // POST: GetOverallScore
        [HttpPost]
        public JsonResult GetOverallScore(EvaluationRequest request)
        {
            if (string.IsNullOrEmpty(request.StudentInput) || string.IsNullOrEmpty(request.SectionInfo))
            {
                return Json(new { success = false, message = "Both student input and section information are required." });
            }

            int score = CalculateOverallScore(request.StudentInput, request.SectionInfo);
            string feedbackMessage = GenerateFeedbackMessage(score);

            return Json(new { success = true, overallScore = score, feedback = feedbackMessage });
        }

        private int CalculateOverallScore(string studentInput, string sectionInfo)
        {
            var inputWords = studentInput.Split(new[] { ' ', ',', '.', ';', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var sectionWords = sectionInfo.Split(new[] { ' ', ',', '.', ';', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int matches = inputWords.Count(word => sectionWords.Contains(word, StringComparer.OrdinalIgnoreCase));
            int totalWords = sectionWords.Length;

            return (int)(((double)matches / totalWords) * 100);
        }

        private string GenerateFeedbackMessage(int score)
        {
            if (score > 80)
                return "Excellent understanding of the section!";
            if (score > 50)
                return "Good understanding, but there are areas to improve.";
            return "You need to study this section more carefully.";
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
