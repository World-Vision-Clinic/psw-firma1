using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Integration.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.MedicalRecords.Services
{
 
    public class MedicineService
    {
        private IMedicinesRepository medicineRepository;
        private IMedicalRecordsRepository medicalRecordRepository;
        private IExaminationRepository examinationRepository;
        public MedicineService(IMedicinesRepository medicineRepository, IMedicalRecordsRepository medicalRecordRepository, IExaminationRepository examinationRepository)
        {
            this.medicineRepository = medicineRepository;
            this.medicalRecordRepository = medicalRecordRepository;
            this.examinationRepository = examinationRepository;
        }

        public List<string> GetAllMedicines()
        {
            return medicineRepository.GetAllMedicines();
        }

        public List<string> GetAllIngredients()
        {
            return medicineRepository.GetAllIngredients();
        }
        public Medicine GetById(string id)
        {
           return medicineRepository.GetByID(id);
        }
        public virtual bool AddOrderedMedicine(Medicine orderedMedicine)
        {
            foreach (Medicine medicine in GetAll())
            {
                if (medicine.Name.ToLower().Equals(orderedMedicine.Name.ToLower()))
                {
                    medicine.Quantity += orderedMedicine.Quantity;
                    medicineRepository.SaveChanges();
                    return true;
                }
            }
            Add(orderedMedicine);
            return true;
        }
        public void UpdateMedicine(Medicine medicine)
        {
            medicineRepository.EditMedicine(medicine);
        }
        public void Add(Medicine medicine)
        {
            medicineRepository.Add(medicine);
        }

        public List<Medicine> GetAll()
        {
            return medicineRepository.GetAll();
        }
        public void SaveMedicine(Medicine medicine)
        {
            medicineRepository.Save(medicine);
        }
        public void DeleteMedicine(Medicine medicine)
        {
            medicineRepository.Delete(medicine.ID);
        }

        public void EditMedicine(Medicine medicine)
        {
            medicineRepository.EditMedicine(medicine);
        }

        public List<Medicine> GetConsumedMedicineInPeriod(DateTime startDate, DateTime endDate)
        {
            List<Medicine> medicines = new List<Medicine>();
            foreach (MedicalRecord record in medicalRecordRepository.GetAll())
            {
                record.Examination = examinationRepository.GetAllByMedicalRecordId(record.MedicalRecordID);
                foreach (Examination examination in record.Examination)
                {
                    if (examination.dateOfExamination.Date >= startDate.Date && examination.dateOfExamination.Date <= endDate.Date)
                    {
                        Therapy therapie = examinationRepository.GetTherapyById(examination.TherapyId);
                        if(therapie!=null)
                            medicines.Add(medicineRepository.GetByID(therapie.MedicineId));
                    }
                }
            }
            return medicines;
        }

        public void CreateConsumedMedicinesInPeriodFile(DateTime startDate, DateTime endDate)
        {
            StreamWriter writer = new StreamWriter("consumed-medicine.txt");
            writer.WriteLine("Medicine consumption report for World vision clinic\n\nReport for date period between " + startDate.Date.ToShortDateString()
                + " and " + endDate.Date.ToShortDateString());

            List<Medicine> consumedMedicine = GetConsumedMedicineInPeriod(startDate, endDate);
            if(consumedMedicine.Count==0)
            {
                writer.WriteLine("Between this period there were no medicine consumed.");
            }
            foreach (Medicine medicine in consumedMedicine)
            {
                writer.WriteLine("Name: " + medicine.Name + ", quantity: " + medicine.Quantity);
            }
            writer.Close();
        }

        public bool IsMedicineIDUnique(string id)
        {
            List<Medicine> medicines = GetAll();
            foreach (Medicine medicine in medicines)
                if (medicine.ID.Equals(id))
                    return false;
            
            return true;
        }

        public List<Ingredient> ReadIngredients()
        {
            string[] lines2 = System.IO.File.ReadAllLines("..\\..\\Files\\ingredients.txt");
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (string line in lines2)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Name = line;
                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        public List<Medicine> SetReplacementMedicine(Medicine medicine)
        {
            List<Medicine> supstituteDrugs = new List<Medicine>();
            foreach (string medicID in medicine.ReplacementMedicineIDs)
            {
                supstituteDrugs.Add(GetById(medicID));
            }
            return supstituteDrugs;
        }

        public void SaveMedicineSubstitutes(List<Medicine> substituteDrugs, Medicine changedMedicine)
        {
            foreach (Medicine medicine in substituteDrugs)
            {
                changedMedicine.AddMedicineID(medicine.ID);
            }
            UpdateMedicine(changedMedicine);
        }

        public bool AlreadyInSubstituteDrugs(Medicine selectedMedic, List<Medicine> substituteDrugs)
        {
            foreach (Medicine m in substituteDrugs)
                if (m.ID.Equals(selectedMedic.ID))
                    return true;
            return false;
        }

        public bool ContainsIngredient(Medicine medicine, Ingredient selectedIngredient)
        {
            foreach (Ingredient i in medicine.Ingredient)
                if (i.Name.Equals(selectedIngredient.Name))
                    return true;
            return false;
        }

        public bool AlergicToIngredients(Medicine medicToBeAdded,MedicalRecord medicalRecord)
        {
            foreach (string ingredient in medicalRecord.Allergen.IngredientNames)
            {
                foreach (Ingredient medicToBeAddedIngredients in medicToBeAdded.Ingredient)
                {
                    if (ingredient.Equals(medicToBeAddedIngredients.Name))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AllergicToMedic(Medicine medicToBeAdded,MedicalRecord medicalRecord)
        {
            foreach (string medicineName in medicalRecord.Allergen.MedicineNames)
            {
                if (medicineName.Equals(medicToBeAdded.Name))
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
