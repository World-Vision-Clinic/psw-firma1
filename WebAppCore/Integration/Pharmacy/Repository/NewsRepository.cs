using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Repository
{
   public class NewsRepository : INewsRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<News> GetAll()
        {
            List<News> news = new List<News>();
            dbContext.News.ToList().ForEach(pieceOfNews => news.Add(pieceOfNews));
            return news;
        }

        public News GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(News news)
        {
            dbContext.News.Add(news);
            dbContext.SaveChanges();
        }

        public void Update()
        {
            dbContext.SaveChanges();
        }
    }
}
