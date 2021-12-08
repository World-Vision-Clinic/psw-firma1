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

        public bool IsCanceled { get; set; }
        public bool IsFinished { get; set; }
        public bool IsUpcoming { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }

        public Appointment(int id, int patientId, int doctorId, DateTime date, bool isCanceled, bool isDone, bool isUpcoming, AppointmentType type)
        {
            this.Id = id;
            this.PatientForeignKey = patientId;
            this.DoctorForeignKey = doctorId;
            this.Date = date;
            this.IsCanceled = false;
            this.IsFinished = false;
            this.IsUpcoming = false;
            this.Type = AppointmentType.Appointment;

        }

        public Appointment() { }
    }
}
