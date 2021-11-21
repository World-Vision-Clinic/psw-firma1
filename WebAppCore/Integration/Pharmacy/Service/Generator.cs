﻿using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Integration.Pharmacy.Service
{
    public class Generator
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
        public static string GenerateObjectionId()
        {
            IntegrationDbContext context = new IntegrationDbContext();
            Reply foundedReply = null;
            string objectionId = "";
            do
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                objectionId = Convert.ToBase64String(key);
                foundedReply = context.Replies.SingleOrDefault(reply => reply.ObjectionId == objectionId);

            } while (foundedReply != null  || objectionId.Contains("+"));

            return objectionId;
        }

        public static string GenerateNewsId()
        {
            IntegrationDbContext context = new IntegrationDbContext();
            News foundedNews = null;
            string newsId = "";
            do
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                newsId = Convert.ToBase64String(key);
                foundedNews = context.News.SingleOrDefault(news => news.IdEncoded == newsId);

            } while (foundedNews != null);

            return newsId;
        }
        public static string GenerateMedicineId()
        {
            IntegrationDbContext context = new IntegrationDbContext();
            Medicine foundedMedicine = null;
            string medicineId = "";
            do
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                medicineId = Convert.ToBase64String(key);
                foundedMedicine = context.Medicines.SingleOrDefault(medicine => medicine.ID == medicineId);

            } while (foundedMedicine != null || medicineId.Contains("+"));

            return medicineId;
        }
    }
}
