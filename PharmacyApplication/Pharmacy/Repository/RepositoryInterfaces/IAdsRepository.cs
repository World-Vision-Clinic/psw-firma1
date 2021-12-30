using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface IAdsRepository
    {
        public List<Ad> GetAll();
        public void AddAd(Ad ad);
        public void DeleteAd(long adId);
    }
}
