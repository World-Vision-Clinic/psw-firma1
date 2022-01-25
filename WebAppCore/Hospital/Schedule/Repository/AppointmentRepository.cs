using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalContext _context;
        
        public AppointmentRepository() {}

        public AppointmentRepository(HospitalContext context)
        {
            _context = context;
        }
        public List<Appointment> GetAll()
        {
            return null;
        }
        public List<Appointment> GetByPatientId(int patientId)
        {
            return null;
        }
        public List<Appointment> GetByDoctorId(int doctorId)
        {
            return null;
        }

        public List<Appointment> GetByDoctorId(int doctorId, DateRange dateRange)
        {
            return null;
        }

        public List<Appointment> GetByDoctorType(DoctorType type)
        {
            List<Doctor> matchingDoctors = _context.Doctors.Where(d => d.Type == type).ToList();
            List<Appointment> appointments = new List<Appointment>();
            foreach (Doctor d in matchingDoctors)
            {
                appointments.AddRange(GetByDoctorId(d.Id));
            }
            return appointments;
        }
        public void AddAppointment(Appointment newAppointment)
        {
            SaveSync();
        }
        public List<Appointment> GetByDoctorIdAndDate(int id, DateTime date) {
            return null;
        }

        public Appointment FindById(int id)
        {
            return null;
        }

        public void Modify(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            SaveSync();
        }

        public void SaveSync()
        {
            _context.SaveChanges();
        }
    }
}
