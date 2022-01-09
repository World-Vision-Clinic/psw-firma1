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
            return _context.Appointments.ToList();
        }
        public List<Appointment> GetByPatientId(int patientId)
        {
            return _context.Appointments.Where(f => f.PatientForeignKey == patientId).ToList();
        }
        public List<Appointment> GetByDoctorId(int doctorId)
        {
            return _context.Appointments.Where(f => f.DoctorForeignKey == doctorId).ToList();
        }

        //Graficki editor
        public List<Appointment> GetByRoomId(int roomId)
        {
            return _context.Appointments.Where(f => f.RoomId == roomId).ToList();
        }


        public List<Appointment> GetByDoctorId(int doctorId, DateTime lowerDateRange, DateTime upperDateRange)
        {
            return _context.Appointments.Where(f => f.DoctorForeignKey == doctorId && f.Date >= lowerDateRange && f.Date < upperDateRange).ToList();
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
            _context.Appointments.Add(newAppointment);
            SaveSync();
        }
        public List<Appointment> GetByDoctorIdAndDate(int id, DateTime date) {
            return _context.Appointments.Where(f => f.DoctorForeignKey == id && f.Date.Date.Equals(date.Date)).ToList();
        }

        public Appointment FindById(int id)
        {
            return _context.Appointments.FirstOrDefault(p => p.Id == id);
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
