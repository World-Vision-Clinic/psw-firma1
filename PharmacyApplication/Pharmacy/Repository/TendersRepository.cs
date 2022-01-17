using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Repository
{
    public class TendersRepository : ITendersRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();
        public List<Tender> GetAll()
        {
            List<Tender> tenders = new List<Tender>();
            dbContext.Tenders.ToList().ForEach(tend => tenders.Add(tend));
            return tenders;
        }

        public void Save(Tender tender)
        {
            dbContext.Tenders.Add(tender);
            dbContext.SaveChanges();
        }

        public Tender GetById(string tenderId)
        {
            Tender tender = dbContext.Tenders.ToList().FirstOrDefault(tender => tender.TenderHash == tenderId);
            if (tender == null)
            {
                return null;
            }
            return tender;
        }

        public bool Update(Tender editedTender)
        {
            Tender tender = dbContext.Tenders.ToList().FirstOrDefault(tender => tender.TenderId == editedTender.TenderId);
            if (tender == null)
            {
                return false;
            }

            tender.TenderHash = editedTender.TenderHash;
            tender.HospitalName = editedTender.HospitalName;
            tender.Title = editedTender.Title;
            tender.Description = editedTender.Description;
            tender.TenderItems = editedTender.TenderItems;
            tender.EndTime = editedTender.EndTime;
            tender.TenderOffers = editedTender.TenderOffers;

            dbContext.Tenders.Update(tender);
            dbContext.SaveChanges();
            return true; 
        }

        public void CloseTender(Tender arrivedTender)
        {
            Tender tender = dbContext.Tenders.ToList().FirstOrDefault(tender => tender.TenderHash == arrivedTender.TenderHash);
            tender.EndTime = arrivedTender.EndTime;
            dbContext.SaveChanges();
        }


    }
}
