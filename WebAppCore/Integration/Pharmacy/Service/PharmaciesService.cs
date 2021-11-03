using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Service
{
    public class PharmaciesService
    {
        IntegrationDbContext context = new IntegrationDbContext();

        public bool AddNewPharmacy(PharmacyProfile newPharmacy)
        {
            newPharmacy.Key = Generator.GenerateApiKey();
            PharmacyProfile foundedPharmacy = context.Pharmacies.SingleOrDefault(pharmacy => pharmacy.Localhost == newPharmacy.Localhost);
            if(foundedPharmacy != null)
            {
                return false;
            }

            context.Pharmacies.Add(newPharmacy);
            context.SaveChanges();
            return true;
        }
    }
}
