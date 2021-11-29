using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Appointment
    {
       // private Doctor doctor;
       // private Room room;
        private AppointmentType type;
        public int Id { get; set; }
        public int PatientForeignKey { get; set; }
        public int DoctorForeignKey { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
