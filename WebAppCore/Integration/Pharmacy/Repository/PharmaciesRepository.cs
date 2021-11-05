using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    class PharmaciesRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();

        public PharmacyProfile Get(String id)
        {
            PharmacyProfile foundedPharmacy = dbContext.Pharmacies.SingleOrDefault(pharmacy => pharmacy.Localhost == id);
            return foundedPharmacy;
        }

        public void Save(PharmacyProfile pharmacy)
        {
            dbContext.Pharmacies.Add(pharmacy);
            dbContext.SaveChanges();
        }

        public List<PharmacyProfile> GetAll()
        {
            return dbContext.Pharmacies.ToList();
        }
    }
}
