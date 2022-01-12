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

            return dbContext.Tenders.Include("TenderItems").Include("TenderOffers").Include("TenderOffers.OfferItems").ToList();
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

       
      
        public List<TenderOffer> GetAllTenderOffersWithOfferItems()
        {
            List<TenderOffer> offers = new List<TenderOffer>();
            foreach(Tender tender in GetAll())
            {
                foreach(TenderOffer offer in tender.TenderOffers)
                {
                    offers.Add(offer);
                }
            }
            return offers;
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
            tender.AddTenderOffer(tenderOffer.TenderOfferHash, tenderOffer.TenderHash, tenderOffer.PharmacyName, tenderOffer.TotalPrice, tenderOffer.OfferItems, tenderOffer.Winner);
            dbContext.SaveChanges();
            return true;
        }

        public Tender GetByTenderHash(string id)
        {
            return dbContext.Tenders.Include("TenderItems").SingleOrDefault(tender => tender.TenderHash == id);
        }

        public TenderOffer GetTenderOfferWithOfferItems(string pharmacyName, string offerHash)
        {
            List<TenderOffer> offers = GetAllTenderOffersWithOfferItems();
            TenderOffer offer = new TenderOffer();
            foreach(TenderOffer o in offers)
            {
                if(o.PharmacyName == pharmacyName && o.TenderOfferHash == offerHash)
                {
                    offer = o;
                    break;
                }
            }

            return offer;
        }

    }
}
