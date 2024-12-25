using DBMSCourse.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DBMSCourse.Models;

namespace DBMSCourse.Controllers
{
    public class QuizPageController : Controller
    {
        private readonly QuizRepository _quizRepository;
        private readonly OverallReportRepository _reportRepository;

        public QuizPageController()
        {
            _quizRepository = new QuizRepository(new DBMSCourseContext());
            _reportRepository = new OverallReportRepository(new DBMSCourseContext());
        }

        // GET: QuizPage
        public ActionResult QuizPage(int? sectionId)
        {
            ViewBag.SectionId = sectionId;
            return View();
        }

        [HttpPost]
        public JsonResult CheckAnswer(int quizId, string selectedOption)
        {
            var quiz = _quizRepository.GetQuizById(quizId);
            if (quiz == null)
            {
                return Json(new { success = false, message = "Quiz not found." });
            }

            bool isCorrect = string.Equals(quiz.CorrectAnswer.ToLower(), selectedOption.ToLower());
            return Json(new { success = true, isCorrect });
        }

        [HttpPost]
        public JsonResult UpdateCorrectAnswerCount(int sectionId, int numberOfCorrectAnswer)
        {
            try
            {
                var report = _reportRepository.GetOverallReportBySectionId(sectionId);
                var quiz = _quizRepository.GetQuizzesBySection(sectionId);
                var quizCount = quiz.Count;

                double quizScore = (double)numberOfCorrectAnswer / quizCount * 100;
                if (report != null)
                {
                    report.QuizScore = Convert.ToDecimal(quizScore);
                    _reportRepository.UpdateOverallReportBySectionId(sectionId,report);
                    
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Report not found." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
