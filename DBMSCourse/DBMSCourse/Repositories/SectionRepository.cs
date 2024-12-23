﻿using DBMSCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBMSCourse.Repositories
{
    public class SectionRepository
    {
        private readonly DBMSCourseDBContext _dbContext;

        public SectionRepository(DBMSCourseDBContext context)
        {
            _dbContext = context;
        }

        public List<Section> GetAllSections()
        {
            return _dbContext.Sections.ToList();
        }

        public string GetSectionInfoById(int? sectionId)
        {
            return _dbContext.Sections.FirstOrDefault(s => s.SectionId == sectionId).DetailedInfo;
        }
        
        public Section GetSectionById(int? sectionId)
        {
            return _dbContext.Sections.FirstOrDefault(s => s.SectionId == sectionId);
        }
    }
}