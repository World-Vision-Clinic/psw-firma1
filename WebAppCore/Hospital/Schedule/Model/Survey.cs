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
