using Integration.Partnership.Model;
using Integration.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Partnership.Repository.RepositoryInterfaces
{
    public interface ITenderRepository : IGenericRepository<Tender>
    {
        List<TenderOffer> GetAllTenderOffers();
        public bool AddOffer(TenderOffer tenderOffer);
        public void EditTenderByHash(Tender tender);
        public void EditTenderOfferById(TenderOffer offer);
    }
}
