using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Survey 
    {
        private int id;
        private DateTime creationDate;
        private int idAppointment;
        //public List<SurveyQuestion> Questions {get; set;}  //od svih pitanja koja postoje u bazi ovo ce biti lista pitanja koja ce se naci na nekoj odredjenoj anketi 
       
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public int IdAppointment
        {
            get { return idAppointment; }
            set { idAppointment = value;
            }
        }

       

    }
}
