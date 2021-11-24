﻿using Pharmacy.Model;
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

        public bool AddMedicine(Medicine medinice)
        {
            return repository.AddMedicine(medinice);
        }

        public bool DeleteMedicine(long medicineId)
        {
            return repository.DeleteMedicine(medicineId);
        }

        public bool UpdateMedicine(Medicine medicine)
        {
            return repository.UpdateMedicine(medicine);
        }

        public bool ProcureMedicine(long medicineId, int quantity)
        {
            Medicine medicine = repository.GetById(medicineId);
            if (medicine == null)
            {
                return false;
            }

            medicine.Quantity -= quantity;
            repository.UpdateMedicine(medicine);
            return true;

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
                if (med.MedicineName.Equals(medicine.MedicineName) && med.Weigth == medicine.Weigth)
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

        public string GetSpecification(Medicine medicine)
        {
            string specificaton = "Name: " + medicine.MedicineName + "\n\n";
            specificaton += "Weigth: " + medicine.Weigth + "mg\n\n";
            specificaton += "Manufacturer: " + medicine.Manufacturer + "\n\n";
            specificaton += "SideEffects: " + medicine.SideEffects + "\n\n";
            specificaton += "Usage: " + medicine.Usage + "\n\n";
            specificaton += "Main precautions: " + medicine.MainPrecautions + "\n\n";
            specificaton += "Potential dangers: " + medicine.PotentialDangers + "\n\n";
            specificaton += "Substances: ";
            foreach (Substance substance in medicine.Substances)
            {
                specificaton += substance.Name + ", ";
            }

            specificaton = specificaton.Substring(0, specificaton.Length - 2);
            specificaton += "\n\n";

            specificaton += "Substitute medicines: ";
            foreach (SubstituteMedicine substitute in medicine.SubstituteMedicines)
            {
                specificaton += substitute.Substitute.MedicineName + " " + substitute.Substitute.Weigth + "mg, ";
            }

            specificaton = specificaton.Substring(0, specificaton.Length - 2);

            return specificaton;
        }
    }
}
