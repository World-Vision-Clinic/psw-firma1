using Hospital.Schedule.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public class SurveyRepository
    {
        private HospitalContext _context;


        public SurveyRepository(HospitalContext context)
        {
            _context = context;
        }

        public void AddSurvey(Survey newSurvey)
        {
            _context.Surveys.Add(newSurvey);
            SaveSurvey();
        }

        public void SaveSurvey()
        {
            _context.SaveChanges();
        }

        public Survey FindById(int id)
        {
            return _context.Surveys.Find(id);
        }

        public bool SurveyExists(int id)
        {
            return _context.Surveys.Any(s => s.Id == id);
        }

        public List<Survey> GetAll()
        {
            return _context.Surveys.ToList();
        }
    }
}
