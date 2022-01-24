using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital.Schedule.Service;
using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Service;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class DoctorStatsMapper
    {
        internal static DoctorStatDTO getDoctorStats(Doctor doctor, DateTime startDate, DateTime endDate,  VacationService vacationService, OnCallShiftService onCallShiftService, AppointmentService appointmentService)
        {

            DoctorStatDTO stat = new DoctorStatDTO();
            stat.Doctor = doctor;
            if (doctor.Id == 1)
            {
                _ = 0;
            }
            stat.NumberOfAppointments = appointmentService.getNumberOfAppointments(doctor, startDate, endDate);
            stat.NumberOfOnCallShifts = onCallShiftService.getNumberOfOnCallShifts(doctor, startDate, endDate);
            stat.NumberOfPatients = appointmentService.getNumberOfPatients(doctor, startDate, endDate);
            stat.NumberOfVacationDays = vacationService.getNumberOfVacationDays(doctor, startDate, endDate);
            if (doctor.Id == 1)
            {
                _ = 0;
            }

            return stat;
        }
    }
}
