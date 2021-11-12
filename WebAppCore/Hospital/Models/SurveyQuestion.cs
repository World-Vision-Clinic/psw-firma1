using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Models
{
    public class SurveyQuestion
    {
        private int id;
        private int answer;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Answer
        {
            get { return answer; }
            set { answer = value; }
        }
        
    }
}
