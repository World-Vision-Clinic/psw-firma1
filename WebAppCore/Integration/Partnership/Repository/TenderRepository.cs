using Integration.Partnership.Model;
using Integration.Partnership.Repository.RepositoryInterfaces;
using Integration.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Partnership.Repository
{
    public class TenderRepository : ITenderRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Tender> GetAll()
        {
            return dbContext.Tenders.Include("TenderOffers").ToList();
        }
        
        public List<TenderOffer> GetAllTenderOffers()
        {
            return dbContext.TenderOffers.ToList();
        }

        public Tender GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Tender parameter)
        {
            dbContext.Tenders.Add(parameter);
            dbContext.SaveChanges();
        }
    }
}
