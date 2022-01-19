using Hospital.MedicalRecords.Model;
using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Appointment : Entity
    {
        public int Id { get; set; }
        public int PatientForeignKey { get; set; }
        public int DoctorForeignKey { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Length { get; set; }
        public bool IsCancelled { get; set; }

        //public int IdSurvey {get; set;}
        public virtual ICollection<Survey> Surveys { get; set; }

        public Appointment(int id, int patientId, int doctorId, DateTime date)
        {
            this.Id = id;
            this.PatientForeignKey = patientId;
            this.DoctorForeignKey = doctorId;
            this.Date = date;
            this.IsCancelled = false;
            this.Type = AppointmentType.Appointment;
        }

        public Appointment(int id, int patientId, int doctorId, DateTime date, bool isCancelled, AppointmentType type)
        {
            this.Id = id;
            this.PatientForeignKey = patientId;
            this.DoctorForeignKey = doctorId;
            this.Date = date;
            this.IsCancelled = isCancelled;
            this.Type = type;
        }

        public bool OverlapsWith(Appointment appointment)
        {
            if (Date.Date.Equals(appointment.Date.Date))
            {
                TimeRange timeRange = new TimeRange(Date.TimeOfDay, Date.TimeOfDay + Length);
                TimeRange otherTimeRange = new TimeRange(appointment.Date.TimeOfDay, appointment.Date.TimeOfDay + appointment.Length);
                if (timeRange.OverlapsWith(otherTimeRange))
                    return true;
            }
            return false;
        }

        public bool OverlapsWith(DateTime otherDate, TimeSpan otherLength)
        {
            DateTime rawDate1 = Date.Date;
            DateTime rawDate2 = otherDate.Date;
            if (rawDate1.Equals(rawDate2))
            {
                TimeRange timeRange = new TimeRange(Date.TimeOfDay, Date.TimeOfDay + Length);
                TimeRange otherTimeRange = new TimeRange(otherDate.TimeOfDay, otherDate.TimeOfDay + otherLength);
                if (timeRange.OverlapsWith(otherTimeRange))
                    return true;
            }
            return false;
        }

        public Appointment() { }
    }
}
