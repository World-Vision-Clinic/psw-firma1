using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class SurveySection
    {
        private SurveyQuestion question;
        private int id;

        public SurveyQuestion Question
        {
            get { return question; }
            set { question = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
