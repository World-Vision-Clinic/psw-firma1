using Pharmacy.Model;
using Pharmacy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pharmacy.Service
{
    public class Generator
    {
        public static string GenerateApiKey()
        {
            PharmacyDbContext dbContext = new PharmacyDbContext();
            Hospital foundedHospital = null;
            string apiKey = "";
            do
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                apiKey = Convert.ToBase64String(key);
                foundedHospital = dbContext.Hospitals.SingleOrDefault(pharmacy => pharmacy.Key == apiKey);

            } while (foundedHospital != null);

            return apiKey;
        }

        public static string GenerateNewsId()
        {
            PharmacyDbContext dbContext = new PharmacyDbContext();
            News foundedNews = null;
            string newsId = "";
            do
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                newsId = Convert.ToBase64String(key);
                foundedNews = dbContext.News.SingleOrDefault(news => news.IdEncoded == newsId);

            } while (foundedNews != null);

            return newsId;
        }
    }
}
