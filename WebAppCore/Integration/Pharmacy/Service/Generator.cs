﻿using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Integration.Pharmacy.Service
{
    class Generator
    {
        public static string GenerateApiKey()
        {
            IntegrationDbContext context = new IntegrationDbContext();
            PharmacyProfile foundedPharmacy = null;
            string apiKey = "";
            do {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                apiKey = Convert.ToBase64String(key);
                foundedPharmacy = context.Pharmacies.SingleOrDefault(pharmacy => pharmacy.Key == apiKey);

            } while (foundedPharmacy != null);

            return apiKey;
        }
    }
}
