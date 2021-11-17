﻿using Hospital.Models;
using Hospital.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service
{
    public class SurveyService
    {
        private  SurveyRepository _repo;

        public SurveyService(SurveyRepository repo)
        {
            _repo = repo;
        }

        public List<Survey> GetAll()
        {
            return _repo.GetAll();
        }

        public void AddSurvey(Survey newSurvey)
        {
            _repo.AddSurvey(newSurvey);
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
