using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class AllergenRepository : IAllergenRepository
    {
        private readonly HospitalContext _context;

        public AllergenRepository()
        {
        }

        public AllergenRepository(HospitalContext context)
        {
            _context = context;
        }

        public void SaveSync()
        {
            _context.SaveChanges();
        }

        public Allergen FindById(int id)
        {
            return _context.Allergens.FirstOrDefault(d => d.Id == id);
        }

        public List<Allergen> GetAllergens()
        {
            return _context.Allergens.ToList();
        }

        public List<Allergen> GetAllergensFromList(List<int> allergensIds)
        {
            List<Allergen> allergens = new List<Allergen>();
            foreach(int id in allergensIds)
            {
                Allergen allergen = FindById(id);
                allergens.Add(allergen);
            }
            return allergens;
        }
    }
}
