using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Appointment
    {
        private int id;
        private DateTime date;
        private TimeSpan time;
        private int duration;
        private Patient patient;
       // private Doctor doctor;
       // private Room room;
        private AppointmentType type;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }



    }
}
