using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public enum Gender
    {
        MALE,
        FEMALE
    }
    public class Patient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }
        public bool Activated { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Residence { get; set; }
        public string ContactPhone { get; set; }
        public string DoctorName { get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }
        public string BloodType { get; set; }

        public Patient() { }

    }
}
