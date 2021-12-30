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
        IMedicineRepository medicineRepository;

        public AdsService(IAdsRepository repository)
        {
            adsRepository = repository;
            medicineRepository = new MedicineRepository();
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

    }
}
