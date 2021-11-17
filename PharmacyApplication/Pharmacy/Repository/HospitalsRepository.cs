using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Repository
{
    public class HospitalsRepository : IHospitalsRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();
        public List<Hospital> GetAll()
        {
            List<Hospital> hospitals = new List<Hospital>();
            dbContext.Hospitals.ToList().ForEach(hospital => hospitals.Add(hospital));
            return hospitals;
        }

        public void Save(Hospital hospital)
        {
            dbContext.Hospitals.Add(hospital);
            dbContext.SaveChanges();
        }
    }
}
