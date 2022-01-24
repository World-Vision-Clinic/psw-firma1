using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public interface IReportRepository
    {
        public void SaveSync();
        public Report FindById(int id);
        public List<Report> FindByAppointmentId(int appointmentId);
    }
}
