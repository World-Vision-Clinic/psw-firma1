using Hospital;
using Hospital.Repositories.Interfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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
