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
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public Appointment GetByDateAndDoctor(DateTime date, TimeSpan time, int doctorId)
        {
            List<Appointment> allAppointments = _patientRepository.GetAllAppointments();
            return allAppointments
                   .Where(g => g.OverlapsWith(date, time)).ToList().FirstOrDefault();
        }

        public List<Appointment> GetAvailableByDateRangeAndDoctor(DateRange dateRange, TimeRange timeRange, int doctorId, AppointmentSearchPriority priority = AppointmentSearchPriority.NO_PRIORITY)
        {
            DateTime lowerDateRange = new DateTime(dateRange.From.Year, dateRange.From.Month, dateRange.From.Day, 0, 0, 0);
            DateTime upperDateRange = new DateTime(dateRange.To.Year, dateRange.To.Month, dateRange.To.Day, 23, 59, 59);
            dateRange = new DateRange(lowerDateRange, upperDateRange);

            int appointmentDuration = 30;

            List<Appointment> doctorAppointments = _patientRepository.GetAppointmentsByDoctorId(doctorId, dateRange);
            List<Appointment> freeAppointments = GenerateFreeAppointmentList(dateRange, timeRange, new TimeSpan(0, appointmentDuration, 0), doctorId);

            List<Appointment> freeAppointmentsFiltered = FilterFreeAppointmentsByDoctorAvailability(freeAppointments, doctorAppointments);
            FillAppointmentsWithDoctorId(freeAppointmentsFiltered, doctorId);

            if (freeAppointmentsFiltered.Count <= 0)
            {
                if (priority == AppointmentSearchPriority.DOCTOR_PRIORITY)
                {
                    int range = 5;
                    int minimumDaysCount = 2;
                    DateTime minimumDate = DateTime.Now.Date.AddDays(minimumDaysCount);
                    DateTime extendedLowerRange = lowerDateRange.AddDays(-range);
                    DateTime extendedUpperRange = upperDateRange.AddDays(range);

                    DateTime beforeLowerRange = (extendedLowerRange > minimumDate ? extendedLowerRange : minimumDate);
                    DateRange beforeDateRange = new DateRange(beforeLowerRange, lowerDateRange.AddHours(23).AddMinutes(59)); //TODO: Testirati ovo

                    List<Appointment> freeAppointmentsBefore = GenerateFreeAppointmentList(beforeDateRange, timeRange, new TimeSpan(0, appointmentDuration, 0), doctorId);
                    List<Appointment> doctorAppointmentsBefore = _patientRepository.GetAppointmentsByDoctorId(doctorId, beforeDateRange);
                    List<Appointment> freeAppointmentsFilteredBefore = FilterFreeAppointmentsByDoctorAvailability(freeAppointmentsBefore, doctorAppointmentsBefore);

                    DateRange afterDateRange = new DateRange(upperDateRange.Date.AddDays(1), extendedUpperRange.Date);

                    List<Appointment> freeAppointmentsAfter = GenerateFreeAppointmentList(afterDateRange, timeRange, new TimeSpan(0, 30, 0), doctorId);
                    List<Appointment> doctorAppointmentsAfter = _patientRepository.GetAppointmentsByDoctorId(doctorId, afterDateRange);
                    List<Appointment> freeAppointmentsFilteredAfter = FilterFreeAppointmentsByDoctorAvailability(freeAppointmentsAfter, doctorAppointmentsAfter);

                    freeAppointmentsFiltered = freeAppointmentsFilteredBefore.Concat(freeAppointmentsFilteredAfter).ToList();
                    FillAppointmentsWithDoctorId(freeAppointmentsFiltered, doctorId);
                }
                else
                {
                    Doctor doctor = _doctorRepository.FindById(doctorId);
                    List<Doctor> matchingDoctors = _doctorRepository.GetDoctorsByType(doctor.Type);

                    foreach (Doctor d in matchingDoctors)
                    {
                        if (d.Id != doctorId)
                        {
                            doctorAppointments = _patientRepository.GetAppointmentsByDoctorId(d.Id, dateRange);
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
            foreach (Appointment a in freeAppointments)
            {
                bool overlapFound = false;
                foreach (Appointment da in doctorAppointments)
                {
                    
                    //if (!da.IsCancelled && DatesOverlap(a.Date, a.Length, da.Date, da.Length)) //TODO: Da li koristiti overlap funkciju? - proveriti
                    if (!da.IsCancelled && a.OverlapsWith(da))
                    {
                        overlapFound = true;
                        break;
                    }
                }
                if (!overlapFound)
                    freeAppointmentsFiltered.Add(a);
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
                appointment.Length = appointmentLenght;
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

        private bool OverlapsWithAppointments(Appointment appointment, List<Appointment> takenAppointments)
        {
            foreach (Appointment ta in takenAppointments)
            {
                if (appointment.OverlapsWith(ta))
                    return true;
            }
            return false;
        }

        public List<Appointment> GenerateFreeAppointments(int id, DateTime date, List<Appointment> doctorsAppointments)
        {
            date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
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

        public List<Appointment> GenerateFreeAppointmentList(DateRange dateRange, TimeRange timeRange, TimeSpan appointmentLength, int doctorId = 0, bool includeWeekends = false)
        {
            List<Appointment> freeAppointmentList = new List<Appointment>();
            DateTime dayIterator = dateRange.From;
            while (dayIterator <= dateRange.To)
            {
                DateTime timeIterator = dayIterator;
                while (timeIterator.Day == dayIterator.Day)
                {
                    TimeSpan fromTime = timeIterator - dayIterator;
                    TimeSpan toTime = fromTime + appointmentLength;
                    if (toTime.Days > 0)
                    {
                        break;
                    }
                    TimeRange currentTimeRange = new TimeRange(fromTime, toTime);
                    if (timeRange.OverlapsWith(currentTimeRange))
                    {
                        Appointment appointment = new Appointment();
                        appointment.Type = AppointmentType.Appointment;
                        appointment.DoctorForeignKey = doctorId;
                        appointment.PatientForeignKey = 0; //TODO: Vidi sta je ovo
                        appointment.Date = timeIterator;
                        appointment.Length = appointmentLength;

                        freeAppointmentList.Add(appointment);
                    }

                    timeIterator += appointmentLength;
                }
                dayIterator = dayIterator.AddDays(1);
            }
            return freeAppointmentList;
        }
    }
}
