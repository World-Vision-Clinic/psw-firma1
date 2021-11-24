using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class AnsweredSurveyQuestion
    {
        public int SurveyForeignKey { get; set; }
        public int PatientForeignKey { get; set; }
        public string Question { get; set; }
        public SurveySectionType Section { get; set; }
        public int Answer { get; set; } 
        public int Id { get; set; }

        public AnsweredSurveyQuestion() { }

        public AnsweredSurveyQuestion( int surveyKey, int patientKey, string question, SurveySectionType section, int answer)
        {
            this.SurveyForeignKey = surveyKey;
            this.PatientForeignKey = patientKey;
            this.Question = question;
            this.Section = section;
            this.Answer = answer;
        }


    }
}
