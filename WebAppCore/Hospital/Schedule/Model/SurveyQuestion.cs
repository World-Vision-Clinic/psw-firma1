using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class SurveyQuestion
    {
        private int id;
        private string question; //tekst pitanja
        private SurveySectionType section; //sekcija kojoj ce pitanje pripadati
        

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Question
        {
            get { return question; }
            set { question = value; }
        }
        
       public SurveySectionType Section
       {
           get { return section; }
           set { section = value; }
       }

    }
}
