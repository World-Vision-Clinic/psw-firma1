using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class PatientRegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
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
        public List<int> Allergens { get; set; }
        public int PreferedDoctor { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string BloodType { get; set; }

        public PatientRegisterDTO() { }

        public Patient ToPatient()
        {
            return new Patient(UserName, Password, new FullName(FirstName, LastName), EMail, false, (Gender)Enum.Parse(typeof(Gender), Gender),
                Jmbg, DateOfBirth, new Residence(Country, Address, City), Phone, PreferedDoctor, Weight, Height, (BloodType)Enum.Parse(typeof(BloodType), BloodType), false);
        }
    }
}
