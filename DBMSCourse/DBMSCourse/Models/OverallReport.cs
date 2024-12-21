using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBMSCourse.Models
{
    public class OverallReport
    {
        [Key]
        public int ReportId { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public static double QuizScore { get; set; }
        public static double KnowladgeCheckScore { get; set; }
        public double? SectionOverallScore { get; set; } = GetOverall();

        private static double GetOverall() => (QuizScore * 0.3) + (KnowladgeCheckScore * 0.7);
    }
}