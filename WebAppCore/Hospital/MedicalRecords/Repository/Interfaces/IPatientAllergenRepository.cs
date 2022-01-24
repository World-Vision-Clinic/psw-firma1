using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IPatientAllergenRepository
    {
        public void SaveSync();
        public PatientAllergen FindById(int id);
        public List<Allergen> FindByPatientId(int patientId);
        public List<Patient> FindByAllergenId(int allergenId);
        public void AddPatientAllergen(int patientId, int allergenId);
    }
}
