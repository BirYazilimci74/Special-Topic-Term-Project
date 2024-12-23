using System.ComponentModel.DataAnnotations;

namespace DBMSCourse.Models
{
    public class OverallReport
    {
        [Key]
        public int ReportId { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public decimal? QuizScore { get; set; } = decimal.Zero;
        public decimal? KnowledgeCheckScore { get; set; } = decimal.Zero;
        public decimal? SectionOverallScore { get; set; } = decimal.Zero;
    }
}