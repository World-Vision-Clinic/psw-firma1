using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Repository
{
   public interface IMedicinesRepository
    {
        List<string> GetAllMedicines();
        List<string> GetAllIngredients();
        public void Add(Medicine medicine);
        void EditMedicine(Medicine editedMedicine);
        public void SaveChanges();
        public Medicine GetByID(string id);
        List<Medicine> GetAll();
        void Save(Medicine parameter);
        void Delete(string id);
    }
}
