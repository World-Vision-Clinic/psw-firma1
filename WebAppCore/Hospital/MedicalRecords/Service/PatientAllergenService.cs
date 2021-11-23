using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class PatientAllergenService
    {
        private readonly IPatientAllergenRepository _repo;

        public PatientAllergenService(IPatientAllergenRepository repo)
        {
            _repo = repo;
        }

        public void AddPatientAllergen(int patientId, int allergenId)
        {
            _repo.AddPatientAllergen(patientId, allergenId);
        }

        public PatientAllergen FindById(int id)
        {
            return _repo.FindById(id);
        }

        public List<Allergen> FindByPatientId(int patientId)
        {
            return _repo.FindByPatientId(patientId);
        }
        public List<Patient> FindByAllergenId(int allergenId)
        {
            return _repo.FindByAllergenId(allergenId);
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }

    }
}
