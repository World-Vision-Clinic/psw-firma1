using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Service
{
    public class PharmaciesService
    {
        PharmaciesRepository pharamaciesRepository = new PharmaciesRepository();

        public bool AddNewPharmacy(PharmacyProfile newPharmacy, out string generatedKey)
        {
            generatedKey = Generator.GenerateApiKey();
            newPharmacy.Key = generatedKey;
            PharmacyProfile foundedPharmacy = pharamaciesRepository.Get(newPharmacy.Localhost);
            if(foundedPharmacy != null)
            {
                return false;
            }

            pharamaciesRepository.Save(newPharmacy);
            return true;
        }

        public PharmacyProfile Get(string id)
        {
            return pharamaciesRepository.Get(id);
        }

        public List<PharmacyProfile> GetAll()
        {
            return pharamaciesRepository.GetAll();
        }
    }
}
