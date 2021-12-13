using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum BloodType
    {
        A,
        B,
        AB,
        O
    }
    public class Patient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }
        public bool Activated { get; set; }
        public Gender Gender { get; set; }
        public string Jmbg { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int PreferedDoctor { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public BloodType BloodType { get; set; }
        public bool IsBlocked { get; set; }

        public Patient() { }

    }
}
