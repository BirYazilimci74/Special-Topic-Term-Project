﻿using DBMSCourse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DBMSCourse.Repositories
{
    public class OverallReportRepository
    {
        private readonly DBMSCourseContext _dbContext;
        public OverallReportRepository(DBMSCourseContext dBContext)
        { 
            _dbContext = dBContext;
        }

        public List<OverallReport> GetOverallReports()
        {
            return _dbContext.OverallReport.Include(r => r.Section).ToList();
        }

        public OverallReport GetOverallReportBySectionId(int? sectionId)
        {
            return _dbContext.OverallReport.FirstOrDefault(r => r.SectionId == sectionId);
        }

        public void UpdateOverallReportBySectionId(int? sectionId, OverallReport updatedReport)
        {
            var reportToUpdate = _dbContext.OverallReport.FirstOrDefault(s => s.SectionId == sectionId);

            reportToUpdate.QuizScore = updatedReport?.QuizScore ?? reportToUpdate.QuizScore;
            reportToUpdate.KnowledgeCheckScore = updatedReport?.KnowledgeCheckScore ?? reportToUpdate.KnowledgeCheckScore;

            decimal? overallScore = (reportToUpdate.QuizScore * Convert.ToDecimal(0.3)) + (reportToUpdate.KnowledgeCheckScore * Convert.ToDecimal(0.7));
            reportToUpdate.SectionOverallScore = overallScore;

            _dbContext.SaveChanges();
        }
    }
}