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

        public AnsweredSurveyQuestion(int id, int surveyForeignKey, int question, int answer)
        {
            this.Id = id;
            this.SurveyForeignKey = surveyForeignKey;
            this.Question = question;
            this.Answer = answer;
        }
        public AnsweredSurveyQuestion(int surveyForeignKey, int question,  int answer)
        {
            this.SurveyForeignKey = surveyForeignKey;
            this.Question = question;
            this.Answer = answer;
        }
    }
}
