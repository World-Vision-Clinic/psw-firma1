using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IPatientRepository
    {
        public void AddPatient(Patient newPatient);

        public void SaveSync();

        public Patient FindByToken(string token);

        public Patient FindByUserName(string username);

        public Patient FindByEmail(string email);

        public Patient FindById(int id);

        public void Modify(Patient patient);

        public List<Patient> GetAll();
        public Patient FindActivatedByUserName(string username);

        public List<Appointment> GetAllAppointments();
        public Appointment GetAppointmentById(int id);
        public List<Appointment> GetAppointmentsByDoctorId(int id);
        public List<Appointment> GetAppointmentsByDoctorId(int id, DateRange dateRange);
        public List<Appointment> GetAppointmentsByPatientId(int id);
    }
}
