using Hospital.Schedule.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public class SurveySectionRepository
    {
        private HospitalContext _context;

        public SurveySectionRepository(HospitalContext context)
        {
            _context = context;
        }

        public void AddSurveySection(SurveySection newSection)
        {
            _context.SurveySections.Add(newSection);
            SaveSurveySection();
        }

        public void SaveSurveySection()
        {
            _context.SaveChanges();
        }

        public SurveySection FindById(int id)
        {
            return _context.SurveySections.Find(id);
        }

        public bool SurveySectionExists(int id)
        {
            return _context.SurveySections.Any(s => s.Id == id);
        }

        public List<SurveySection> GetAll()
        {
            return _context.SurveySections.ToList();
        }
    }
}
