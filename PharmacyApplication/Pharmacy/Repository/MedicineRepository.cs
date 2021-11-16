using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Pharmacy.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();

        public List<Medicine> GetAll()
        {
            List<Medicine> medicines = new List<Medicine>();
            dbContext.Medicines.ToList().ForEach(medicine => medicines.Add(medicine));
            return medicines;
        }

        public Medicine GetById(long medicineId)
        {
            Medicine medicine = dbContext.Medicines.ToList().FirstOrDefault(medicine => medicine.MedicineId == medicineId);
            if (medicine == null) 
            {
                return null;
            }
            return medicine;
        }
    }
}
