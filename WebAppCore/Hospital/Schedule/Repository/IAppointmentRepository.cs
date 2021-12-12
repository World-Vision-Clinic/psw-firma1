using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public interface IAppointmentRepository
    {
        public void AddAppointment(Appointment newAppointment);

        public void SaveSync();

        public Appointment FindById(int id);


        public void Modify(Appointment patient);
        List<Appointment> GetByPatientId(int v);
        List<Appointment> GetAll();
        List<Appointment> GetByDoctorId(int doctorId, DateTime lowerDateRange, DateTime upperDateRange);
        List<Appointment> GetByDoctorId(int doctorId);
        List<Appointment> GetByDoctorType(DoctorType type);
        List<Appointment> GetByDoctorIdAndDate(int id, DateTime date);
    }
}
