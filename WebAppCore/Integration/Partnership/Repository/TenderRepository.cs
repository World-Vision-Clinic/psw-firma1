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

            List<Tender> allTenders = dbContext.Tenders.Include("TenderOffers").Include("TenderItems").ToList();
            List<TenderOffer> allOffers = GetAllTenderOffersWithOfferItems();
            foreach(Tender tender in allTenders)
            {
                if(tender.TenderOffers.Count!=0)
                {
                    //int id = tender.TenderOffers.ToList()[0].TenderOfferId;
                    foreach(TenderOffer offer in tender.TenderOffers)
                    {
                        int id = offer.TenderOfferId;
                        foreach (TenderOffer ofer in allOffers)
                        {
                            if (id == ofer.TenderOfferId)
                            {
                                offer.OfferItems = ofer.OfferItems;
                            }
                        }
                    }
                }
            }

            return allTenders;
        }

        public void EditTenderEndTimeByHash(Tender tender)
        {
            List<Tender> allTenders = dbContext.Tenders.ToList();
            foreach(Tender t in allTenders)
            {
                if(t.TenderHash==tender.TenderHash)
                {
                    t.EndTime = tender.EndTime;
                    break;
                }
            }
            dbContext.SaveChanges();
        }
        public void EditTenderOfferById(TenderOffer offer)
        {
            List<TenderOffer> allOffers = dbContext.TenderOffers.ToList();
            foreach (TenderOffer o in allOffers)
            {
                if (o.TenderOfferId == offer.TenderOfferId)
                {
                    o.Winner = offer.Winner;
                    break;
                }
            }
            dbContext.SaveChanges();

        }

        public List<TenderOffer> GetAllTenderOffers()
        {
            return dbContext.TenderOffers.ToList();
        }
        public List<OfferItem> GetAllOfferItems()
        {
            return dbContext.OfferItems.ToList();
        }
        public List<TenderOffer> GetAllTenderOffersWithOfferItems()
        {
            return dbContext.TenderOffers.Include("OfferItems").ToList();
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

        public Tender GetByTenderHash(string id)
        {
            return dbContext.Tenders.Include("TenderItems").SingleOrDefault(tender => tender.TenderHash == id);
        }
    }
}
