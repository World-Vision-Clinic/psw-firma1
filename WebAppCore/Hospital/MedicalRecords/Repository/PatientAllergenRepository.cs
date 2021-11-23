using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class PatientAllergenRepository : IPatientAllergenRepository
    {
        private readonly HospitalContext _context;
        private readonly IPatientRepository _patientRepository;
        private readonly IAllergenRepository _allergenRepository;

        public PatientAllergenRepository()
        {
        }

        public PatientAllergenRepository(HospitalContext context, IPatientRepository patientRepository, IAllergenRepository allergenRepository)
        {
            _context = context;
            _patientRepository = patientRepository;
            _allergenRepository = allergenRepository;
        }

        public void SaveSync()
        {
            _context.SaveChanges();
        }

        public void AddPatientAllergen(int patientId, int allergenId)
        {
            PatientAllergen patientAllergen = new PatientAllergen(patientId, allergenId);
            _context.PatientAllergens.Add(patientAllergen);
            SaveSync();
        }

        public PatientAllergen FindById(int id)
        {
            return _context.PatientAllergens.FirstOrDefault(d => d.Id == id);
        }

        public List<Allergen> FindByPatientId(int patientId)
        {
            var query = from pa in _context.PatientAllergens
                        where pa.PatientId == patientId
                        select new
                        {
                            allergenId = pa.AllergenId
                        };
            List<Allergen> allergens = new List<Allergen>();
            foreach(var a in query)
            {
                allergens.Add(_allergenRepository.FindById(a.allergenId));
            }
            return allergens;
        }

        public List<Patient> FindByAllergenId(int allergenId)
        {
            var query = from pa in _context.PatientAllergens
                        where pa.AllergenId == allergenId
                        select new
                        {
                            patientId = pa.PatientId
                        };
            List<Patient> patients = new List<Patient>();
            foreach (var a in query)
            {
                patients.Add(_patientRepository.FindById(a.patientId));
            }
            return patients;
        }
    }
}
