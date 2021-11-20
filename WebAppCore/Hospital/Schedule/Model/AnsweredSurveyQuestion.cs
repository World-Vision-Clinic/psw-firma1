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
        public int Answer { get; set; } //ocena koju mozemo dati
        public int Id { get; set; }




    }
}
