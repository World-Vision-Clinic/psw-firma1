using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Survey 
    {
        [Key]
        public int IdSurvey { get; set; }
        public DateTime CreationDate { get; set; }
        public int IdAppointment { get; set; }

        [ForeignKey("IdAppointment")]
        public virtual Appointment Appointment { get; set; }


        public Survey()
        {
            IdAppointment = 1;
        }

        public Survey(int id, int idAppointment)
        {
            this.IdSurvey = id;
            this.IdAppointment = idAppointment;

        }
       

        
      

       

    }
}
