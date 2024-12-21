using DBMSCourse.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DBMSCourse.Models;

namespace DBMSCourse.Controllers
{
    public class QuizPageController : Controller
    {
        private readonly QuizRepository _repository;

        public QuizPageController()
        {
            _repository = new QuizRepository(new DBMSCourseDBContext());
        }

        // GET: QuizPage
        public ActionResult QuizPage()
        {
            var quizzes = _repository.GetAllQuizzes();
            return View(quizzes);
        }

        [HttpPost]
        public JsonResult CheckAnswer(int quizId, string selectedOption)
        {
            var quiz = _repository.GetQuizById(quizId);
            if (quiz == null)
            {
                return Json(new { success = false, message = "Quiz not found." });
            }

            bool isCorrect = string.Equals(quiz.CorrectAnswer.ToLower(), selectedOption.ToLower());
            return Json(new { success = true, isCorrect });
        }
    }
}
