using Hospital.Schedule.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private HospitalContext _context;

        public SurveyRepository(HospitalContext context)
        {
            _context = context;
        }

        public int AddSurvey(Survey newSurvey)
        {
            _context.Surveys.Add(newSurvey);
            SaveSurvey();
            return newSurvey.Id;
        }

        public void AddSurveyQuestion(SurveyQuestion newQuestion)
        {
            _context.Questions.Add(newQuestion);
            SaveSurvey();
        }

        public void AddAnswer(AnsweredSurveyQuestion answer)
        {
            _context.AnsweredQuestions.Add(answer);
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

        public Survey FindByAppointmentId(int appointmentId)
        {
            return null; //TODO
        }

        public bool SurveyExists(int id)
        {
            return false; // TODO
        }

        public List<Survey> GetAll()
        {
            return _context.Surveys.ToList();
        }

        public List<SurveyQuestion> GetAllQuestions()  
        {
            return _context.Questions.ToList();
        }

        public SurveyQuestion GetQuestionById(int id)
        {
            return _context.Questions.Find(id);
        }

        public List<AnsweredSurveyQuestion> GetAllAnsweredQuestions()
        {
            return _context.AnsweredQuestions.ToList();
        }
    }
}
