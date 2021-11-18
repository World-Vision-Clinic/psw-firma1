using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Repository
{
    public class SubstanceRepository : ISubstanceRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();

        public List<Substance> GetAll()
        {
            List<Substance> substances = new List<Substance>();
            dbContext.Substances.ToList().ForEach(substance => substances.Add(substance));
            return substances;
        }

    }
}