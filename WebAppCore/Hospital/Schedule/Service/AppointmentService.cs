using Hospital.MedicalRecords.Model;
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
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentService(IAppointmentRepository repo, IDoctorRepository doctorRepository)
        {
            _repo = repo;
            _doctorRepository = doctorRepository;
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

            List<Appointment> freeAppointmentsFiltered = FilterFreeAppointmentsByDoctorAvailability(freeAppointments, doctorAppointments);

            if (freeAppointmentsFiltered.Count <= 0)
            {
                if (priority == AppointmentSearchPriority.DOCTOR_PRIORITY)
                {
                    int range = 5;
                    int minimumDaysCount = 2;
                    DateTime minimumDate = DateTime.Now.AddDays(minimumDaysCount);
                    DateTime extendedLowerDate = lowerDateRange.AddDays(-range);
                    DateTime extendedUpperRange = upperDateRange.AddDays(range);
                    List<Appointment> freeAppointmentsBefore = GenerateFreeAppointmentList((extendedLowerDate > minimumDate ? extendedLowerDate : minimumDate),
                        lowerDateRange, lowerTimeRange, upperTimeRange, new TimeSpan(0, 30, 0));
                    List<Appointment> doctorAppointmentsBefore = GetByDoctorId(doctorId, (extendedLowerDate > minimumDate ? extendedLowerDate : minimumDate),
                        lowerDateRange);
                    List<Appointment> freeAppointmentsFilteredBefore = FilterFreeAppointmentsByDoctorAvailability(freeAppointmentsBefore, doctorAppointmentsBefore);

                    List<Appointment> freeAppointmentsAfter = GenerateFreeAppointmentList(upperDateRange.Date, extendedUpperRange.Date, lowerTimeRange, upperTimeRange, new TimeSpan(0, 30, 0));
                    List<Appointment> doctorAppointmentsAfter = GetByDoctorId(doctorId, upperDateRange, extendedUpperRange);
                    List<Appointment> freeAppointmentsFilteredAfter = FilterFreeAppointmentsByDoctorAvailability(freeAppointmentsAfter, doctorAppointmentsAfter);

                    freeAppointmentsFiltered = freeAppointmentsFilteredBefore.Concat(freeAppointmentsFilteredAfter).ToList();
                }
                else
                {
                    Doctor doctor = _doctorRepository.FindById(doctorId);
                    List<Doctor> matchingDoctors = _doctorRepository.GetDoctorsByType(doctor.Type);

                    foreach (Doctor d in matchingDoctors)
                    {
                        if (d.Id != doctorId)
                        {
                            doctorAppointments = GetByDoctorId(d.Id, lowerDateRange, upperDateRange);
                            List<Appointment> newAppointments = FilterFreeAppointmentsByDoctorAvailability(freeAppointments, doctorAppointments);
                            FillAppointmentsWithDoctorId(newAppointments, d.Id);
                            freeAppointmentsFiltered.AddRange(newAppointments);

                        }
                    }
                    return freeAppointmentsFiltered;
                }
            }

            return freeAppointmentsFiltered;
        }

        private void FillAppointmentsWithDoctorId(List<Appointment> appointments, int doctorId)
        {
            foreach (Appointment a in appointments)
            {
                a.DoctorForeignKey = doctorId;
            }
        }

        private List<Appointment> FilterFreeAppointmentsByDoctorAvailability(List<Appointment> freeAppointments, List<Appointment> doctorAppointments)
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

        public void Modify(Appointment appointment)
        {
            _repo.Modify(appointment);
        }
    }
}
