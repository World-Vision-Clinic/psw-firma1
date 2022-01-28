using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int PatientForeignKey { get; set; }
        public int DoctorForeignKey { get; set; }
        public string DoctorName { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; } //TODO: Rename to Length

        public bool IsCancelled { get; set; }
        public bool IsUpcoming { get; set; }
        public bool HasCompletedSurvey { get; set; }

        public AppointmentDTO() { }

        public AppointmentDTO(Appointment appointment, DoctorRepository doctorRepository = null, SurveyRepository surveyRepository = null)
        {
            this.Id = appointment.Id;
            this.PatientForeignKey = appointment.PatientForeignKey;
            this.DoctorForeignKey = appointment.DoctorForeignKey;
            this.DoctorName = GetDoctorName(doctorRepository);
            this.Type = appointment.Type;
            this.Date = appointment.Date;
            this.Time = appointment.Length;
            this.IsCancelled = appointment.IsCancelled;
            this.IsUpcoming = IsAppointmentUpcoming();
            this.HasCompletedSurvey = HasAppointmentCompletedSurvey(appointment, surveyRepository);
        }

        private bool IsAppointmentUpcoming()
        {
            if (Date > DateTime.Now)
                return true;
            return false;
        }

        private bool HasAppointmentCompletedSurvey(Appointment appointment, SurveyRepository surveyRepository = null)
        {
            if (surveyRepository != null && surveyRepository.FindByAppointmentId(appointment.Id) != null)
                return true;
            else
                return false;
        }

        private string GetDoctorName(DoctorRepository doctorRepository = null)
        {
            if (doctorRepository == null)
                return "Undefined Doctor";
            Doctor doctor = doctorRepository.FindById(this.DoctorForeignKey);
            if (doctor == null)
                return "Undefined Doctor";
            return doctor.FirstName + " " + doctor.LastName;
        }
    }
}
