using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class AllergenService
    {
        private readonly IAllergenRepository _repo;

        public AllergenService(IAllergenRepository repo)
        {
            _repo = repo;
        }
        public Allergen FindById(int id)
        {
            int i = 1;
            return _repo.FindById(id);
        }
        public List<Allergen> GetAllergens()
        {
            return _repo.GetAllergens();
        }
        public List<Allergen> GetAllergensFromList(List<int> allergens)
        {
            return _repo.GetAllergensFromList(allergens);
        }
    }
}
