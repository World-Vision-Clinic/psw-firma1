using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientForeignKey { get; set; }
        public int DoctorForeignKey { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public bool IsCancelled { get; set; }

        //public int IdSurvey {get; set;}
        public virtual ICollection<Survey> Surveys { get; set; }

        public Appointment(int id, int patientId, int doctorId, DateTime date, bool isCanceled, bool isDone, bool isUpcoming, AppointmentType type)
        {
            this.Id = id;
            this.PatientForeignKey = patientId;
            this.DoctorForeignKey = doctorId;
            this.Date = date;
            this.IsCancelled = false;
            this.Type = AppointmentType.Appointment;

        }

        public Appointment() { }
    }
}
