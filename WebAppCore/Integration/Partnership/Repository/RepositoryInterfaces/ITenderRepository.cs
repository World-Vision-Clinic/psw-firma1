using Integration.Partnership.Model;
using Integration.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Partnership.Repository.RepositoryInterfaces
{
    public interface ITenderRepository : IGenericRepository<Tender>
    {
        public bool AddOffer(TenderOffer tenderOffer);
        public void EditTenderEndTimeByHash(Tender tender);
        public void EditTenderOfferById(TenderOffer offer);
        public Tender GetByTenderHash(string id);
        public TenderOffer GetTenderOfferWithOfferItems(string tenderHash, string offerHash);
    }
}
