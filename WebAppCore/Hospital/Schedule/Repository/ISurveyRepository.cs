using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public interface ISurveyRepository
    {
        public void AddSurvey(Survey newSurvey);
        public void AddSurveyQuestion(SurveyQuestion newQuestion);
        public void SaveSurvey();
        public Survey FindById(int id);
        public bool SurveyExists(int id);
        public List<Survey> GetAll();
        public List<SurveyQuestion> GetAllQuestions();
        
    }
}
