using Hospital.Schedule.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public class ReportRepository : IReportRepository
    {

        private readonly HospitalContext _context;
        private readonly IAppointmentRepository _appointmentRepository;

        public ReportRepository(HospitalContext context, IAppointmentRepository appointmentRepository)
        {
            _context = context;
            _appointmentRepository = appointmentRepository;
        }


        public List<Report> FindByAppointmentId(int appointmentId)
        {
            return _context.Reports.Where(f => f.AppointmentId == appointmentId).ToList();
        }

        public Report FindById(int id)
        {
            return _context.Reports.FirstOrDefault(d => d.Id == id);

        }
        public void SaveSync()
        {
            _context.SaveChanges();
        }
    }
}
