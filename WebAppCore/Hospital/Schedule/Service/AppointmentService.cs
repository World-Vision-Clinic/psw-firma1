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

        public List<Appointment> GetByDoctorId(int doctorId)
        {
            return _repo.GetByDoctorId(doctorId);
        }

        public List<Appointment> GetByDoctorId(int doctorId, DateTime lowerDateRange, DateTime upperDateRange)
        {
            return _repo.GetByDoctorId(doctorId, lowerDateRange, upperDateRange);
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

        public List<Appointment> GetAvailableByDateRangeAndDoctor(DateTime lowerDateRange, DateTime upperDateRange, TimeSpan lowerTimeRange, TimeSpan upperTimeRange, int doctorId, AppointmentSearchPriority priority = AppointmentSearchPriority.NO_PRIORITY)
        {
            List<Appointment> doctorAppointments = GetByDoctorId(doctorId, lowerDateRange, upperDateRange);
            List<Appointment> freeAppointments = GenerateFreeAppointmentList(lowerDateRange, upperDateRange, lowerTimeRange, upperTimeRange, new TimeSpan(0, 30, 0));

            List<Appointment> freeAppointmentsFiltered = FilterFreeAppointmentsByDoctorOccupation(freeAppointments, doctorAppointments);

            if (freeAppointmentsFiltered.Count <= 0 && priority == AppointmentSearchPriority.DOCTOR_PRIORITY)
            {
                List<Appointment> freeAppointmentsBefore = GenerateFreeAppointmentList((lowerDateRange.AddDays(-5) > DateTime.Today.AddDays(2) ? lowerDateRange.AddDays(-5) : DateTime.Today.AddDays(2)), 
                    lowerDateRange, lowerTimeRange, upperTimeRange, new TimeSpan(0, 30, 0));
                List<Appointment> doctorAppointmentsBefore = GetByDoctorId(doctorId, (lowerDateRange.AddDays(-5) > DateTime.Today.AddDays(2) ? lowerDateRange.AddDays(-5) : DateTime.Today.AddDays(2)),
                    lowerDateRange);
                List<Appointment> freeAppointmentsFilteredBefore = FilterFreeAppointmentsByDoctorOccupation(freeAppointmentsBefore, doctorAppointmentsBefore);

                List<Appointment> freeAppointmentsAfter = GenerateFreeAppointmentList(upperDateRange.Date, upperDateRange.AddDays(5).Date, lowerTimeRange, upperTimeRange, new TimeSpan(0, 30, 0));
                List<Appointment> doctorAppointmentsAfter = GetByDoctorId(doctorId, upperDateRange, upperDateRange.AddDays(5));
                List<Appointment> freeAppointmentsFilteredAfter = FilterFreeAppointmentsByDoctorOccupation(freeAppointmentsAfter, doctorAppointmentsAfter);

                freeAppointmentsFiltered = freeAppointmentsFilteredBefore.Concat(freeAppointmentsFilteredAfter).ToList();
            }

            return freeAppointmentsFiltered;
        }

        private List<Appointment> GenerateEveryAppointmentForWorkday(DateTime workdayBegin , DateTime workdayEnd, int id) 
        {
            TimeSpan appointmentLenght = new TimeSpan(0, 29, 59);
            List<Appointment> appointments = new List<Appointment>();
            DateTime appointmentBegin = workdayBegin;
            while (appointmentBegin < workdayEnd) 
            {
                Appointment appointment = new Appointment();
                appointment.Date = appointmentBegin;
                appointment.Time = appointmentLenght;
                appointment.PatientForeignKey = 1;
                appointment.Type = AppointmentType.Appointment;
                appointment.DoctorForeignKey = id;
                appointments.Add(appointment);
                appointmentBegin = appointmentBegin.AddMinutes(30);
            }
            return appointments;
        }

        private List<Appointment> GetFreeAppointments(List<Appointment> possibleAppointments, List<Appointment> takenAppointments) 
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in possibleAppointments)
            {
                if (!OverlapsWithAppointments(appointment,takenAppointments))
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        private bool OverlapsWithAppointments(Appointment appointment,List<Appointment> takenAppointments)
        {
            foreach (Appointment takenAppointment in takenAppointments)
            {
                if (DatesOverlap(appointment.Date, appointment.Time, takenAppointment.Date, takenAppointment.Time))
                    return true;
            }
            return false;
        }

        public List<Appointment> GenerateFreeAppointments(int id, DateTime date, List<Appointment> doctorsAppointments)
        {
            DateTime workdayBegin = date.AddHours(9);
            DateTime workdayEnd = date.AddHours(17);
            List<Appointment> appointments = new List<Appointment>();
            List<Appointment> allPossibleAppointments = GenerateEveryAppointmentForWorkday(workdayBegin,workdayEnd,id);

            if (doctorsAppointments.Count != 0)
            {
                appointments = GetFreeAppointments(allPossibleAppointments, doctorsAppointments);
            }
            else 
            {
                appointments = allPossibleAppointments;
            }
            return appointments;
        }

        public List<Appointment> GetByDoctorIdAndDate(int id, DateTime date)
        {
            return _repo.GetByDoctorIdAndDate(id,date);
        }

        private List<Appointment> FilterFreeAppointmentsByDoctorOccupation(List<Appointment> freeAppointments, List<Appointment> doctorAppointments)
        {
            List<Appointment> freeAppointmentsFiltered = new List<Appointment>();
            foreach (Appointment appointmentIterator in freeAppointments)
            {
                bool overlapFound = false;
                foreach (Appointment doctorAppointmentIterator in doctorAppointments)
                {
                    if (DatesOverlap(appointmentIterator.Date, appointmentIterator.Time, doctorAppointmentIterator.Date, doctorAppointmentIterator.Time))
                    {
                        overlapFound = true;
                        break;
                    }
                }
                if (!overlapFound)
                    freeAppointmentsFiltered.Add(appointmentIterator);
            }
            return freeAppointmentsFiltered;
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
