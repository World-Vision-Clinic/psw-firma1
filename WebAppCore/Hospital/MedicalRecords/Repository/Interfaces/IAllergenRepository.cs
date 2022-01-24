using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IAllergenRepository
    {
        public void SaveSync();
        public Allergen FindById(int id);
        public List<Allergen> GetAllergens();
        public List<Allergen> GetAllergensFromList(List<int> allergens);
    }
}
