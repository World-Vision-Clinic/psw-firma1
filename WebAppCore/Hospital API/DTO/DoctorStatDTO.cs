using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class DoctorStatDTO
    {
        public Doctor Doctor { get; set; }
        public int NumberOfAppointments { get; set; }
        public int NumberOfOnCallShifts { get; set; }
        public int NumberOfPatients { get; set; }
        public int NumberOfVacationDays { get; set; }

        public DoctorStatDTO() { }

        public DoctorStatDTO(Doctor doctor,int numberOfAppointments, int numberOfOnCallShifts, int numberOfPatients, int numberOfVacationDays)
        {
            Doctor = doctor;
            NumberOfAppointments = numberOfAppointments;
            NumberOfOnCallShifts = numberOfOnCallShifts;
            NumberOfPatients = numberOfPatients;
            NumberOfVacationDays = numberOfVacationDays;
        }
    }
}
