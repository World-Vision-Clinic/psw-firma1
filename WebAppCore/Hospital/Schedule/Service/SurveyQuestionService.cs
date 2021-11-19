using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Service
{
    public class SurveyQuestionService
    {
        private SurveyQuestionRepository _repo;

        public SurveyQuestionService(SurveyQuestionRepository repo)
        {
            _repo = repo;
        }

        public List<SurveyQuestion> GetAll()
        {
            return _repo.GetAll();
        }

        public void AddSurveyQuestion(SurveyQuestion newQuestion)
        {
            _repo.AddSurveyQuestion(newQuestion);
        }

        public SurveyQuestion FindById(int id)
        {
            return _repo.FindById(id);
        }

        public bool SurveyQuestionExists(int id)
        {
            return _repo.SurveyQuestionExists(id);
        }

        public void Save()
        {
            _repo.SaveSurveyQuestion();
        }
    }
}
