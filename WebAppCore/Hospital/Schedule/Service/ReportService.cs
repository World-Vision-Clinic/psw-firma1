using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Service
{
    public class ReportService
    {
        private readonly IReportRepository _repo;

        public ReportService(IReportRepository repo)
        {
            _repo = repo;
        }

        public Report FindById(int id)
        {
            return _repo.FindById(id);
        }

        public List<Report> FindByAppointmentId(int appointmentId)
        {
            return _repo.FindByAppointmentId(appointmentId);
        }

    }
}
