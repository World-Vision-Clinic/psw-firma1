using Hospital.MedicalRecords.Model;
using Hospital.ShiftsAndVacations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class DoctorWithShiftDTO
    {
        public Doctor doctor { get; set; }
        public Shift shift { get; set; }

        public DoctorWithShiftDTO(Doctor doc, Shift sh)
        {
            this.doctor = doc;
            this.shift = sh;
        }
        public DoctorWithShiftDTO() { }

    }
}
