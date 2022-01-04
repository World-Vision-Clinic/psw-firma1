using Pharmacy.Repository.RepositoryInterfaces;
using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Pharmacy.Service;
using Xunit;

namespace PharmacyApplicationTests.UnitTests
{
    public class MedicineAdsTests
    {
        private static IAdsRepository CreateAdsStubRepository()
        {
            var stubRepository = new Mock<IAdsRepository>();
            var ads = new List<Ad>();

            Medicine brufen = new Medicine(1L, "Brufen", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 500.00, 400.00, "mainPrecautions1", "potentialDangers1", new List<Substance>(), 200);
            Medicine panadol = new Medicine(2L, "Panadol", "Galenika", "sideEffects2", "usage2", new List<SubstituteMedicine>(), 400.00, 350.00, "mainPrecautions2", "potentialDangers2", new List<Substance>(), 500);
            
            SubstituteMedicine substituteMedicine = new SubstituteMedicine();
            substituteMedicine.Medicine = brufen;
            substituteMedicine.MedicineId = brufen.MedicineId;
            substituteMedicine.Substitute = panadol;
            substituteMedicine.SubstituteId = panadol.MedicineId;
            brufen.SubstituteMedicines.Add(substituteMedicine);

            List<MedicineAd> medicineAds = new List<MedicineAd>();
            MedicineAd medicineAdBrufen = new MedicineAd(1L, brufen.MedicineId, 370.00);
            MedicineAd medicineAdPanadol = new MedicineAd(2L, brufen.MedicineId, 305.00);
            medicineAds.Add(medicineAdBrufen);
            medicineAds.Add(medicineAdPanadol);
            ICollection<MedicineAd> medicineAdsCollection = medicineAds;

            Ad holidaySale = new Ad(1L, "Praznicno snizenje", "U ponudi su neki od lekova nase apoteke", DateTime.Now, new DateTime(2022, 1, 15), medicineAds);
            ads.Add(holidaySale);

            stubRepository.Setup(a => a.GetAll()).Returns(ads);
            return stubRepository.Object;
        }

        [Fact]
        public void Find_ads_repository_size_after_creating_an_ad()
        {
            IAdsRepository stubRepository = CreateAdsStubRepository();
            AdsService service = new AdsService(stubRepository);

            Medicine andol = new Medicine(3L, "Andol", "Galenika", "sideEffects3", "usage3", new List<SubstituteMedicine>(), 400.00, 280.00, "mainPrecautions3", "potentialDangers3", new List<Substance>(), 300);
            List<MedicineAd> medicineAdsAndol = new List<MedicineAd>();
            MedicineAd medicineAdAndol = new MedicineAd(3L, andol.MedicineId, 220.00);
            medicineAdsAndol.Add(medicineAdAndol);
            Ad testAd = new Ad(2L, "Popust za nase korisnike", "Clanovi koji imaju nasu karticu imaju popust na Andol", DateTime.Now, new DateTime(2022, 1, 16), medicineAdsAndol);
            service.Add(testAd);
            
            List<Ad> ads = service.GetAll();
            int repoSize = ads.Count;

            Assert.Equal("1", repoSize.ToString());
        }

         [Fact]
        public void Find_ads_repository_size_after_deleting_an_ad()
        {
            IAdsRepository stubRepository = CreateAdsStubRepository();

            AdsService service = new AdsService(stubRepository);
            service.Delete(1L);
            Ad ad = service.GetById(1L);

            Assert.Null(ad);
        }
    }
}
