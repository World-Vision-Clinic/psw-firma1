using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Service
{
    public class SurveyService
    {
        private ISurveyRepository _repo;

        public SurveyService(ISurveyRepository repo)
        {
            _repo = repo;
        }

        public List<Survey> GetAll()
        {
            return _repo.GetAll();
        }

        public List<SurveyQuestion> GetAllQuestions()
        {
            return _repo.GetAllQuestions();
        }

        public int AddSurvey(Survey newSurvey)
        {
            return _repo.AddSurvey(newSurvey);
            
        }

        public void AddAnswer(AnsweredSurveyQuestion answer)
        {
            _repo.AddAnswer(answer);
        }

        public Survey FindById(int id)
        {
            return _repo.FindById(id);
        }

        public bool SurveyExists(int id)
        {
            return _repo.SurveyExists(id);
        }

        public void Save()
        {
            _repo.SaveSurvey();
        }
    }
}
