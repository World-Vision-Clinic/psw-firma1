using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Models
{
    public class SurveyQuestion
    {
        private int id;
        private int answer; //ocena koju cemo dati kao odgovor na pitanje

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
