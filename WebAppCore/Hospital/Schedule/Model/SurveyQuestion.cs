using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class SurveyQuestion
    {
        [Key]
        private int id;
        private string question; //tekst pitanja
        private SurveySectionType section; //sekcija kojoj ce pitanje pripadati
        public int Answer { get; set; } //ocena koju mozemo dati, ovo mi zbog mapiranja na frontu treba
        public int IdSurvey { get; set; }

        public SurveyQuestion() { }

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
