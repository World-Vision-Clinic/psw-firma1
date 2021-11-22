using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Service
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public void AddAppointment(Appointment newAppointment)
        {
            _repo.AddAppointment(newAppointment);
        }

        public Appointment FindById(int id)
        {
            return _repo.FindById(id);
        }

        public List<Appointment> GetByPatientId(int patientId)
        {
            return _repo.GetByPatientId(patientId);
        }

        public List<Appointment> GetAll()
        {
            return _repo.GetAll();
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }
    }
}
