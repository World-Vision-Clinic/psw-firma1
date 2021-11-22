using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class MedicineService
    {
        IMedicineRepository repository;

        public MedicineService(IMedicineRepository medicineRepository)
        {
            repository = medicineRepository;
        }

        public List<Medicine> GetAll() 
        {
            return repository.GetAll();
        }

        public bool OrderMedicine(Medicine medicine)
        {
            foreach (Medicine med in GetAll())
            {
                if (med.MedicineName.Equals(medicine.MedicineName))
                {
                    med.Quantity -= medicine.Quantity;
                    if (med.Quantity == 0)
                    {
                        DeleteMedicine(med.MedicineId);
                        return true;
                    }
                    repository.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Medicine GetById(long medicineId) 
        {
            List<Medicine> medicines = repository.GetAll();
            foreach(Medicine medicine in medicines)
            {
                if(medicine.MedicineId == medicineId)
                {
                    return medicine;
                }
            }
            return null;
        }

        public Boolean AddMedicine(Medicine medinice)
        {
            return repository.AddMedicine(medinice);
        }

        public Boolean DeleteMedicine(long medicineId)
        {
            return repository.DeleteMedicine(medicineId);
        }

        public Boolean UpdateMedicine(Medicine medicine)
        {
            return repository.UpdateMedicine(medicine);
        }

        public Boolean ProcureMedicine(long medicineId, int quantity)
        {
            return repository.ProcureMedicine(medicineId, quantity);
        }

        public List<string> FoundReplacements(Medicine medicine)
        {
            List<string> replacementsId = new List<string>();
            foreach (Medicine med in GetAll())
            {
                if (med.MedicineName.Equals(medicine.MedicineName))
                {
                    foreach (SubstituteMedicine sm in med.SubstituteMedicines)
                    {
                        replacementsId.Add(sm.Substitute.MedicineName);
                    }
                }
            }
            return replacementsId;
        }

        public Medicine FoundOrderedMedicine(Medicine medicine)
        {
            foreach (Medicine med in GetAll())
            {
                if (med.MedicineName.Equals(medicine.MedicineName))
                {
                    return med;
                }
            }
            return null;
        }
        public List<Medicine> GetByName(string name) //Does not have to be full name
        { 
            List<Medicine> medicines = new List<Medicine>();

            foreach (Medicine m in repository.GetAll())
            {
                if (m.MedicineName.ToLower().Contains(name.ToLower()))
                {
                    medicines.Add(m);
                }
            }

            return medicines;

        }

        public List<Medicine> GetByFullName(string name) //Has to be full name (there are medicines with same name but diferent weight)
        {

            List<Medicine> medicines = new List<Medicine>();

            foreach (Medicine m in repository.GetAll())
            {
                if (m.MedicineName.ToLower().Trim().Equals(name.ToLower()))
                {
                    medicines.Add(m);
                }
            }

            return medicines;

        }

        public List<Medicine> ConvertIdsToMedicines(List<long> ids)
        {
            List<Medicine> medicines = new List<Medicine>();

            foreach(long l in ids)
            {
                medicines.Add(GetById(l));
            }

            return medicines;
        }

        public Medicine GetByNameAndWeight(string name, double weight)
        {
            List<Medicine> medicines = GetByFullName(name);
            
            foreach(Medicine m in medicines)
            {
                if (m.Weigth == weight) return m;
            }

            return null;
        }

        public Boolean CheckQuantity(string name, double weight, int quantity)
        {
            Medicine medicine = GetByNameAndWeight(name, weight);
            
            if(medicine!= null && medicine.Quantity >= quantity)
            {
                return true;
            }
            
            return false;
        }


    }
}
