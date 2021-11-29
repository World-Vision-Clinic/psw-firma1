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
    }
}
