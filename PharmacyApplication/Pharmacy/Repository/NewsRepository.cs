using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository
{
   public class NewsRepository : INewsRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();
        public void Save(News news)
        {
            dbContext.News.Add(news);
            dbContext.SaveChanges();
        }
    }
}
