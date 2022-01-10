using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class OnCallShiftDTO
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }

        public OnCallShiftDTO() { }
        public OnCallShiftDTO(int id, Doctor doctor, DateTime date)
        {
            Id = id;
            Doctor = doctor;
            Date = date;
        }
    }
}
