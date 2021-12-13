using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public enum DoctorType
    {
        Ophthalmologist,
        Cardiologist,
        Radiologist,
        Gynecologists,
        Family_physician
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Doctor() { }
        public DoctorType Type { get; set; }

    }
}
