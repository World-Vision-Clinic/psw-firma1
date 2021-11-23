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
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
        public Boolean AddMedicine(Medicine newMedicine)
        {
            Medicine medicine = dbContext.Medicines.ToList().FirstOrDefault(medicine => medicine.MedicineId == newMedicine.MedicineId);
            if(medicine != null)
            {
                return false;
            }

            dbContext.Medicines.Add(newMedicine);
            dbContext.SaveChanges();
            return true;
        }

        public Boolean DeleteMedicine(long medicineId)
        {
            Medicine medicine = dbContext.Medicines.ToList().FirstOrDefault(medicine => medicine.MedicineId == medicineId);
            if(medicine == null)
            {
                return false;
            }
            dbContext.Medicines.Remove(medicine);
            dbContext.SaveChanges();
            return true;
        }

        public Boolean UpdateMedicine(Medicine editedMedicine)
        {
            Medicine medicine = dbContext.Medicines.ToList().FirstOrDefault(medicine => medicine.MedicineId == editedMedicine.MedicineId);
            if (medicine == null)
            {
                return false;
            }

            medicine.MedicineId = editedMedicine.MedicineId;
            medicine.MedicineName = editedMedicine.MedicineName;
            medicine.Manufacturer = editedMedicine.Manufacturer;
            medicine.SideEffects = editedMedicine.SideEffects;
            medicine.Usage = editedMedicine.Usage;
            medicine.SubstituteMedicines = editedMedicine.SubstituteMedicines;
            medicine.Weigth = editedMedicine.Weigth;
            medicine.MainPrecautions = editedMedicine.MainPrecautions;
            medicine.PotentialDangers = editedMedicine.PotentialDangers;
            medicine.Substances = editedMedicine.Substances;
            medicine.Quantity = editedMedicine.Quantity;

            dbContext.Medicines.Update(medicine);
            dbContext.SaveChanges();
            return true;
        }


        
    }
}
