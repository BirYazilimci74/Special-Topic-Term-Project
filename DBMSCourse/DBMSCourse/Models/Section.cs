using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBMSCourse.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string DetailedInfo { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

}