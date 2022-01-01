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
        public bool AddOffer(TenderOffer tenderOffer)
        {
            Tender tender = dbContext.Tenders.Include("TenderItems").SingleOrDefault(tender => tender.TenderHash == tenderOffer.TenderHash);
            if (tender == null) return false;
            if (tender.TenderOffers == null) tender.TenderOffers = new List<TenderOffer>();
            tender.TenderOffers.Add(tenderOffer);
            dbContext.SaveChanges();
            return true;
        }
    }
}
