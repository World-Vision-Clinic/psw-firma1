using Integration.Repositories.Interfaces;
using Integration.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository.RepositoryInterfaces
{
   public interface IPharmaciesRepository : IGenericRepository<PharmacyProfile>
   {
        public PharmacyProfile Get(string id);
        public List<PharmacyProfile> GetFiltered(string searchFilter);

   }
}
