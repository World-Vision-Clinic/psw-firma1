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
        public void AddAppointment(Appointment newAppointment)
        {
            _context.Appointments.Add(newAppointment);
            SaveSync();
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
