using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public class PatientAllergen
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AllergenId { get; set; }

        public PatientAllergen() { }
        public PatientAllergen(int patientId, int allergenId)
        {
            PatientId = patientId;
            AllergenId = allergenId;
        }
    }
}
