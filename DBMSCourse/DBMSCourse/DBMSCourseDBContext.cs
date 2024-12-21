using DBMSCourse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DBMSCourse
{
    public class DBMSCourseDBContext: DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<OverallReport> OverallReport { get; set; }
    }
}