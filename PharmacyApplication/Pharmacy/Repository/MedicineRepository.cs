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
        public bool OrderMedicine(Medicine medicine) 
        {
            foreach(Medicine med in dbContext.Medicines.ToList())
            {
                if (med.MedicineName.Equals(medicine.MedicineName))
                {
                    med.Quantity -= medicine.Quantity;
                    if(med.Quantity == 0)
                    {
                        dbContext.Medicines.Remove(med);
                        dbContext.SaveChanges();
                        return true;
                    }
                    dbContext.SaveChanges();
                    return true;
                }
            }    
            return false;
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

        public bool AddMedicine(Medicine newMedicine)
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

        public bool DeleteMedicine(long medicineId)
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

        public bool UpdateMedicine(Medicine editedMedicine)
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


        public bool ProcureMedicine(long medicineId, int quantity)
        {
            Medicine medicine = dbContext.Medicines.ToList().FirstOrDefault(medicine => medicine.MedicineId == medicineId);
            if(medicine == null)
            {
                return false;
            }

            medicine.Quantity -= quantity;
            dbContext.Medicines.Update(medicine);
            dbContext.SaveChanges();
            return true;

        }
    }
}
