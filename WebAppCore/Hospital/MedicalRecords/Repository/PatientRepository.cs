using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalContext _context;

        public PatientRepository()
        {
            _context = new HospitalContext();
        }

        public PatientRepository(HospitalContext context)
        {
            _context = context;
        }

        public void AddPatient(Patient newPatient)
        {
            _context.Patients.Add(newPatient);
            SaveSync();
        }

        public void SaveSync()
        {
            _context.SaveChanges();
        }

        public Patient FindByToken(string token)
        {
            return _context.Patients.FirstOrDefault(p => String.Equals(p.Token, token));
        }

        public Patient FindByUserName(string username)
        {
            try
            { 
                return _context.Patients.FirstOrDefault(p => String.Equals(p.UserName, username));
            }
            catch
            {
                return null;
            }
        }

        public Patient FindByEmail(string email)
        {
            return _context.Patients.FirstOrDefault(p => String.Equals(p.EMail, email));
        }

        public Patient FindById(int id)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public void Modify(Patient patient)
        {
            _context.ChangeTracker.Clear();
            _context.Patients.Update(patient);
            SaveSync();
        }

        public List<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public Patient FindActivatedByUserName(string username) 
        {
            try
            {
                return _context.Patients.FirstOrDefault(p => String.Equals(p.UserName, username) && p.Activated);
            }
            catch
            {
                return null;
            }
        }

        public List<Appointment> GetAllAppointments()
        {
            List<Patient> allPatients = GetAll();
            List<Appointment> allAppointments = new List<Appointment>();
            foreach (Patient p in allPatients)
                foreach (Appointment a in p.Appointments)
                    allAppointments.Add(a);
            return allAppointments;
        }
        public Appointment GetAppointmentById(int id)
        {
            return GetAllAppointments().Where(p => p.Id == id).FirstOrDefault();
        }
        public List<Appointment> GetAppointmentsByDoctorId(int id)
        {
            return GetAllAppointments().Where(p => p.DoctorForeignKey == id).ToList();
        }
        public List<Appointment> GetAppointmentsByDoctorId(int id, DateRange dateRange)
        {
            return GetAllAppointments().Where(f => f.DoctorForeignKey == id && f.Date >= dateRange.From && f.Date < dateRange.To).ToList();
        }
        public List<Appointment> GetAppointmentsByPatientId(int id)
        {
            return GetAllAppointments().Where(p => p.PatientForeignKey == id).ToList();
        }
        public Appointment GetAppointmentByDateAndDoctor(DateTime date, TimeSpan time, int doctorId)
        {
            List<Appointment> allAppointments = GetAllAppointments();
            return allAppointments
                   .Where(g => g.OverlapsWith(date, time)).ToList().FirstOrDefault();
        }
        public List<Appointment> GetByDoctorType(DoctorType type)
        {
            List<Doctor> matchingDoctors = _context.Doctors.Where(d => d.Type == type).ToList();
            List<Appointment> appointments = new List<Appointment>();
            foreach (Doctor d in matchingDoctors)
            {
                appointments.AddRange(GetAppointmentsByDoctorId(d.Id));
            }
            return appointments;
        }
    }
}
