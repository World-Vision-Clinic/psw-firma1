using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Service
{
    public class PharmaciesService
    {
        private IPharmaciesRepository pharmaciesRepository;

        public PharmaciesService(IPharmaciesRepository pharmaciesRepository)
        {
            this.pharmaciesRepository = pharmaciesRepository;
        }

        public bool AddNewPharmacy(PharmacyProfile newPharmacy, out string generatedKey)
        {
            generatedKey = Generator.GenerateApiKey();
            newPharmacy.Key = generatedKey;
            PharmacyProfile foundedPharmacy = pharmaciesRepository.Get(newPharmacy.Localhost);
            if (foundedPharmacy != null)
            {
                return false;
            }

            pharmaciesRepository.Save(newPharmacy);
            return true;
        }

        public PharmacyProfile Get(string id)
        {
            return pharmaciesRepository.Get(id);
        }

        public List<PharmacyProfile> GetAll()
        {
            return pharmaciesRepository.GetAll();
        }

        public List<PharmacyProfile> GetFiltered(string searchFilter)
        {
            List<PharmacyProfile> pharmacies = new List<PharmacyProfile>();
            foreach (PharmacyProfile pp in GetAll())
            {
                if (pp.Address.ToLower().Contains(searchFilter.ToLower()) || pp.City.ToLower().Contains(searchFilter.ToLower()))
                {
                    pharmacies.Add(pp);
                }
            }
            return pharmacies;
        }
    }
}
