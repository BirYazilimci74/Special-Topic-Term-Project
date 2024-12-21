﻿using DBMSCourse;
using DBMSCourse.Models;
using System.Collections.Generic;
using System.Linq;

public class QuizRepository
{
    private readonly DBMSCourseDBContext _context;

    public QuizRepository(DBMSCourseDBContext context)
    {
        _context = context;
    }

    public List<Quiz> GetAllQuizzes()
    {
        return _context.Quizzes.ToList();
    }

    public Quiz GetQuizById(int id)
    {
        return _context.Quizzes.FirstOrDefault(q => q.QuestionId == id);
    }
}
