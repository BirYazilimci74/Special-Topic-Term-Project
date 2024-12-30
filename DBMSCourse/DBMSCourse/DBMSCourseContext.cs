using DBMSCourse.Models;
using System.Data.Entity;

namespace DBMSCourse
{
    public class DBMSCourseContext:DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<OverallReport> OverallReport { get; set; }
    }
}