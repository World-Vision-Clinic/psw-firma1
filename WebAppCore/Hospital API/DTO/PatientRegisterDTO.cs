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
            Patient patient = new Patient();
            patient.UserName = UserName;
            patient.Password = Password;
            patient.FirstName = FirstName;
            patient.LastName = LastName;
            patient.Token = null;
            patient.Activated = false;
            patient.Gender = (Gender) Enum.Parse(typeof(Gender), Gender);
            patient.Jmbg = Jmbg;
            patient.DateOfBirth = DateOfBirth;
            patient.Country = Country;
            patient.Address = Address;
            patient.Phone = Phone;
            patient.Allergens = Allergens;
            patient.PreferedDoctor = PreferedDoctor;
            patient.Weight = Weight;
            patient.Height = Height;
            patient.BloodType = (BloodType) Enum.Parse(typeof(BloodType), BloodType);

            return patient;
        }
    }
}
