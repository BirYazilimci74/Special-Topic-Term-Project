using DBMSCourse;
using DBMSCourse.Models;
using System.Collections.Generic;
using System.Linq;

namespace DBMSCourse.Repositories
{
    public class QuizRepository
    {
        private readonly DBMSCourseContext _context;

        public QuizRepository(DBMSCourseContext context)
        {
            _context = context;
        }

        public List<Quiz> GetAllQuizzes()
        {
            return _context.Quizzes.ToList();
        }

        public List<Quiz> GetQuizzesBySection(int? sectionId)
        {
            return _context.Quizzes.Where(q => q.SectionId == sectionId).ToList();
        }

        public Quiz GetQuizById(int id)
        {
            return _context.Quizzes.FirstOrDefault(q => q.QuestionId == id);
        }
    }

}
