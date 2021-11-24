using Integration.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    public class MedicineTherapyRepository : IMedicineTherapyRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<MedicineTherapy> GetAll()
        {
            throw new NotImplementedException();
        }

        public MedicineTherapy GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(MedicineTherapy parameter)
        {
            throw new NotImplementedException();
        }
    }
}
