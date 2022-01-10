using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class SurveyQuestion : Entity
    {
        public string Question { get; private set; }
        public SurveySectionType Section { get; private set; }

        public SurveyQuestion() { }

        public SurveyQuestion(int id, string question, SurveySectionType section) 
        {
            this.Id = id;
            this.Question = question;
            this.Section = section;
        }
    }
}
