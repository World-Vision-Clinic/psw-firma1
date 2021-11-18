using Integration;
using Integration.Repositories.Interfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    public class MedicinesRepository : IMedicinesRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void EditMedicine(Medicine editedMedicine)
        {
            throw new NotImplementedException();
        }

        public List<Medicine> GetAll()
        {
            return dbContext.Medicines.ToList();
        }
        public virtual bool AddOrderedMedicine(Medicine orderedMedicine)
        {
            foreach (Medicine medicine in dbContext.Medicines.ToList())
            {
                if (medicine.Name.ToLower().Equals(orderedMedicine.Name.ToLower()))
                {
                    medicine.Quantity += orderedMedicine.Quantity;
                    dbContext.SaveChanges();
                    return true;
                }
            }
            dbContext.Medicines.Add(orderedMedicine);
            dbContext.SaveChanges();
            return true;
        } 
        public List<string> GetAllIngredients()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllMedicines()
        {
            throw new NotImplementedException();
        }

        public Medicine GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Medicine parameter)
        {
            throw new NotImplementedException();
        }

    }
}
