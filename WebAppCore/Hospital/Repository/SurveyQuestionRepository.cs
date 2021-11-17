using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Repository
{
    public class SurveyQuestionRepository
    {
        private HospitalContext _context;

        public SurveyQuestionRepository(HospitalContext context)
        {
            _context = context;
        }

        public void AddSurveyQuestion(SurveyQuestion newQuestion)
        {
            _context.SurveyQuestions.Add(newQuestion);
            SaveSurveyQuestion();
        }

        public void SaveSurveyQuestion()
        {
            _context.SaveChanges();
        }

        public SurveyQuestion FindById(int id)
        {
            return _context.SurveyQuestions.Find(id);
        }

        public bool SurveyQuestionExists(int id)
        {
            return _context.SurveyQuestions.Any(s => s.Id == id);
        }

        public List<SurveyQuestion> GetAll()
        {
            return _context.SurveyQuestions.ToList();
        }
    }
}
