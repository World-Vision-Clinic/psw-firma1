using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Service
{
    public enum AppointmentSearchPriority
    {
        NO_PRIORITY,
        DOCTOR_PRIORITY,
        DATE_TIME_PRIORITY
    }
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

        public Appointment GetByDateAndDoctor(DateTime date, TimeSpan time, int doctorId)
        {
            List<Appointment> allAppointments = GetAll();
            return allAppointments
                   .Where(g => DatesOverlap(g.Date, g.Time, date, time)).ToList().FirstOrDefault();
        }

        public Appointment GetAvailableByDateRangeAndDoctor(DateTime lowerDateRange, DateTime upperDateRange, int doctorId, AppointmentSearchPriority priority = AppointmentSearchPriority.NO_PRIORITY)
        {
            /*List<Appointment> allAppointments = GetAll();
            if(GetByDateAndDoctor())*/
            return null;
        }

        public bool DatesOverlap(DateTime firstDate, TimeSpan firstTimeSpan, DateTime secondDate, TimeSpan secondTimeSpan)
        {
            if (firstDate <= secondDate && firstDate + firstTimeSpan > secondDate)
                return true;
            if (firstDate >= secondDate && firstDate < secondDate + secondTimeSpan)
                return true;
            if (secondDate <= firstDate && secondDate + secondTimeSpan > firstDate)
                return true;
            if (secondDate >= firstDate && secondDate < firstDate + firstTimeSpan)
                return true;
            return false;
        }

        public List<Appointment> GenerateFreeAppointmentList(DateTime lowerDateRange, DateTime upperDateRange, TimeSpan lowerTimeRange, TimeSpan upperTimeRange, TimeSpan appointmentLength, bool includeWeekends = false)
        {
            List<Appointment> freeAppointmentList = new List<Appointment>();
            DateTime dayIterator = lowerDateRange;
            while (dayIterator < upperDateRange)
            {
                DateTime timeIterator = dayIterator;
                while (timeIterator.Day == dayIterator.Day)
                {
                    if (DatesOverlap(dayIterator + lowerTimeRange, upperTimeRange - lowerTimeRange, timeIterator, appointmentLength))
                    {
                        Appointment appointment = new Appointment();
                        appointment.Type = AppointmentType.Appointment;
                        appointment.DoctorForeignKey = 0;
                        appointment.PatientForeignKey = 0;
                        appointment.Date = timeIterator;
                        appointment.Time = appointmentLength;

                        freeAppointmentList.Add(appointment);
                    }
                    timeIterator += appointmentLength;
                }
                dayIterator = dayIterator.AddDays(1);
            }
            return freeAppointmentList;
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }
    }
}
