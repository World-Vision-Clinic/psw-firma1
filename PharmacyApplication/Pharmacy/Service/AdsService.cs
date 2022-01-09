using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class AdsService
    {
        IAdsRepository adsRepository;

        public AdsService(IAdsRepository repository)
        {
            this.adsRepository = repository;
        }
        public List<Ad> GetAll()
        {
            return adsRepository.GetAll();
        }
        public void Add(Ad ad)
        {
            adsRepository.AddAd(ad);
        }
        public void Delete(long adId)
        {
            adsRepository.DeleteAd(adId);
        }
        public Ad GetById(long adId)
        {
            return adsRepository.GetById(adId);
        }

    }
}
