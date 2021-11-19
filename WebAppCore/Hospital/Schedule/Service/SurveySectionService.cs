using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Service
{
    public class SurveySectionService
    {
        private SurveySectionRepository _repo;

        public SurveySectionService(SurveySectionRepository repo)
        {
            _repo = repo;
        }

        public List<SurveySection> GetAll()
        {
            return _repo.GetAll();
        }

        public void AddSurveySection(SurveySection newSection)
        {
            _repo.AddSurveySection(newSection);
        }

        public SurveySection FindById(int id)
        {
            return _repo.FindById(id);
        }

        public bool SurveySectionExists(int id)
        {
            return _repo.SurveySectionExists(id);
        }

        public void Save()
        {
            _repo.SaveSurveySection();
        }
    }
}
