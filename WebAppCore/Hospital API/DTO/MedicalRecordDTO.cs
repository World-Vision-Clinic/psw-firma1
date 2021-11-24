using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class MedicalRecordDTO
    {
        public string UserName { get; set; }
        public string EMail { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Residence { get; set; }
        public string ContactPhone { get; set; }
        public string DoctorName { get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }
        public string BloodType { get; set; }

        public MedicalRecordDTO(Patient patientToCopy) 
        {
            this.UserName = patientToCopy.UserName;
            this.EMail = patientToCopy.EMail;
            this.DateOfBirth = patientToCopy.DateOfBirth;
            this.Gender = patientToCopy.Gender.ToString();
            this.Residence = patientToCopy.Residence;
            this.ContactPhone = patientToCopy.ContactPhone;
            this.DoctorName = patientToCopy.DoctorName;
            this.Height = patientToCopy.Height;
            this.Weight = patientToCopy.Weight;
            this.BloodType = patientToCopy.BloodType;
        }
        public MedicalRecordDTO() { }
    }
}
