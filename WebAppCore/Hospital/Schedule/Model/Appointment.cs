using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Appointment
    {
        private int id;
        private DateTime date;
        private TimeSpan time;
        private int duration;

        public virtual Patient Patient { get; set; }
       // private Doctor doctor;
       // private Room room;
        private AppointmentType type;

        public virtual ICollection<Survey> Surveys { get; set; }

        public Appointment() { }

        [Key]
        public int IdAppointment
        {
            get { return id; }
            set { id = value; }
        }



    }
}
