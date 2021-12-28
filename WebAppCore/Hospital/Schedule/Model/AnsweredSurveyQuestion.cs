using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class AnsweredSurveyQuestion : Entity
    {
        public int SurveyForeignKey { get; private set; }
        public int Question { get; private set; }
        public int Answer { get; private set; }

        public AnsweredSurveyQuestion() { }

        public AnsweredSurveyQuestion(int surveyForeignKey, int question,  int answer, int id = 0)
        {
            this.Id = id;
            this.SurveyForeignKey = surveyForeignKey;
            this.Question = question;
            if (answer < 1 || answer > 5)
                throw new ArgumentException("Rating must be between 1 and 5");
            this.Answer = answer;
        }
    }
}
