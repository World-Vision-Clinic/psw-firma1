﻿using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class MedicalRecordDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Gender { get; set; }
        public string Jmbg { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string PreferedDoctorName { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string BloodType { get; set; }
        public List<string> AllergenList { get; set; }
        public string ProfileImage { get; set; }

        public MedicalRecordDTO(Patient patientToCopy) 
        {
            this.UserName = patientToCopy.UserName;
            this.FirstName = patientToCopy.FullName.FirstName;
            this.LastName = patientToCopy.FullName.LastName;
            this.EMail = patientToCopy.EMail;
            this.Gender = patientToCopy.Gender.ToString();
            this.Jmbg = patientToCopy.Jmbg;
            this.DateOfBirth = patientToCopy.DateOfBirth;
            this.Country = patientToCopy.Residence.Country;
            this.Address = patientToCopy.Residence.Address;
            this.City = patientToCopy.Residence.City;
            this.Phone = patientToCopy.Phone;
            this.PreferedDoctorName = "";
            this.Height = patientToCopy.Height;
            this.Weight = patientToCopy.Weight;
            this.BloodType = patientToCopy.BloodType.ToString();
            this.AllergenList = new List<string>();
            this.ProfileImage = patientToCopy.ProfileImage;
        }
        public MedicalRecordDTO() { }
    }
}
