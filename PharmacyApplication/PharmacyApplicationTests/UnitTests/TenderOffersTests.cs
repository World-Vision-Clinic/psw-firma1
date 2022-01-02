using Moq;
using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using Pharmacy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PharmacyApplicationTests.UnitTests
{
    public class TenderOffersTests
    {

        private static IMedicineRepository CreateMedicineStubRepository()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            var medicines = new List<Medicine>();

            Medicine brufen = new Medicine(1L, "Brufen", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 500.00, "mainPrecautions1", "potentialDangers1", new List<Substance>(), 200);
            brufen.Price = 100;
            Medicine panadol = new Medicine(2L, "Panadol", "Galenika", "sideEffects2", "usage2", new List<SubstituteMedicine>(), 400.00, "mainPrecautions2", "potentialDangers2", new List<Substance>(), 500);
            panadol.Price = 100;
            Medicine andol = new Medicine(3L, "Andol", "Galenika", "sideEffects3", "usage3", new List<SubstituteMedicine>(), 250.00, "mainPrecautions3", "potentialDangers3", new List<Substance>(), 300);
            andol.Price = 100;
            Medicine bromazepam = new Medicine(4L, "Bromazepam", "Galenika", "sideEffects4", "usage4", new List<SubstituteMedicine>(), 1000.00, "mainPrecautions4", "potentialDangers4", new List<Substance>(), 0);
            bromazepam.Price = 100;

            SubstituteMedicine substituteMedicine = new SubstituteMedicine();
            substituteMedicine.Medicine = brufen;
            substituteMedicine.MedicineId = brufen.MedicineId;
            substituteMedicine.Substitute = panadol;
            substituteMedicine.SubstituteId = panadol.MedicineId;

            brufen.SubstituteMedicines.Add(substituteMedicine);

            medicines.Add(brufen);
            medicines.Add(panadol);
            medicines.Add(andol);
            medicines.Add(bromazepam);

            stubRepository.Setup(m => m.GetAll()).Returns(medicines);
            return stubRepository.Object;
        }

        private static ITendersRepository CreateTenderStubRepository()
        {
            var stubRepository = new Mock<ITendersRepository>();
            var tenders = new List<Tender>();
            List<TenderItem> ti = new List<TenderItem>();
            TenderItem item1 = new TenderItem(1, "Brufen", 500.00, 200);
            TenderItem item2 = new TenderItem(2, "Brufen", 200.00, 200);
            TenderItem item3 = new TenderItem(3, "Panadol", 400.00, 700);
            TenderItem item4 = new TenderItem(4, "Andol", 250.00, 350);
            TenderItem item5 = new TenderItem(5, "Bromazepam", 1000.00, 100);
            ti.Add(item1);
            ti.Add(item2);
            ti.Add(item3);
            ti.Add(item4);
            ti.Add(item5);
            ICollection<TenderItem> tenderItems = ti;
            ICollection<TenderItem> tenderItems2 = new List<TenderItem>();
            Tender t1 = new Tender(1, "hash1", "World Vision Clinic", "Tender 1", "Zahtev za lekove", tenderItems, null, new List<TenderOffer>());
            Tender t2 = new Tender(2, "hash2", "World Vision Clinic", "Tender 2", "Zahtev za lekove", tenderItems2, null, new List<TenderOffer>());
            tenders.Add(t1);
            tenders.Add(t2);

            stubRepository.Setup(m => m.GetAll()).Returns(tenders);
            return stubRepository.Object;
        }


        [Fact]
        public void Offer_with_exact_quantity()
        {
            IMedicineRepository medicineRepository = CreateMedicineStubRepository();
            ITendersRepository tenderRepository = CreateTenderStubRepository();
            TenderService service = new TenderService(tenderRepository, medicineRepository);
            Tender tender = service.GetById(1);
            List<OfferItem> items = service.FillOfferItems(tender.TenderItems.ToList());
            //TenderOffer offer = new TenderOffer(service.GenerateTenderOfferHash(), service.GetTotalPrice(items), items); 
            //Assert.Contains(offer.OfferItems, item => (item.MedicineName.Trim().Equals("Brufen") && item.Dosage == 500.00 && item.Quantity == 200));
            Assert.False(items.Count() == 0);
        }

        //[Fact]
        //public void Offer_with_more_quantity()
        //{
        //    IMedicineRepository medicineRepository = CreateMedicineStubRepository();
        //    ITendersRepository tenderRepository = CreateTenderStubRepository();
        //    TenderService service = new TenderService(tenderRepository, medicineRepository);
        //    Tender tender = service.GetById(1);
        //    TenderOffer offer = service.CreateTenderOffer(tender);
        //    Assert.Contains(offer.OfferItems, item => (item.MedicineName.Trim().Equals("Panadol") && item.Dosage == 400.00 && item.Quantity == 500));
        //}

        //[Fact]
        //public void Offer_with_zero_quantity()
        //{
        //    IMedicineRepository medicineRepository = CreateMedicineStubRepository();
        //    ITendersRepository tenderRepository = CreateTenderStubRepository();
        //    TenderService service = new TenderService(tenderRepository, medicineRepository);
        //    Tender tender = service.GetById(1);
        //    TenderOffer offer = service.CreateTenderOffer(tender);
        //    Assert.DoesNotContain(offer.OfferItems, item => (item.MedicineName.Trim().Equals("Bromazepam") && item.Dosage == 1000.0));
        //}

        //[Fact]
        //public void Offer_with_false_dosage()
        //{
        //    IMedicineRepository medicineRepository = CreateMedicineStubRepository();
        //    ITendersRepository tenderRepository = CreateTenderStubRepository();
        //    TenderService service = new TenderService(tenderRepository, medicineRepository);
        //    Tender tender = service.GetById(1);
        //    TenderOffer offer = service.CreateTenderOffer(tender);
        //    Assert.DoesNotContain(offer.OfferItems, item => (item.MedicineName.Trim().Equals("Brufen") && item.Dosage == 200.00));
        //}

        //[Fact]
        //public void Check_count_of_offer_items()
        //{
        //    IMedicineRepository medicineRepository = CreateMedicineStubRepository();
        //    ITendersRepository tenderRepository = CreateTenderStubRepository();
        //    TenderService service = new TenderService(tenderRepository, medicineRepository);
        //    Tender tender = service.GetById(1);
        //    TenderOffer offer = service.CreateTenderOffer(tender);
        //    Assert.True(offer.OfferItems.Count == 3);
        //}

        //[Fact]
        //public void Check_when_tender_items_are_empty()
        //{
        //    IMedicineRepository medicineRepository = CreateMedicineStubRepository();
        //    ITendersRepository tenderRepository = CreateTenderStubRepository();
        //    TenderService service = new TenderService(tenderRepository, medicineRepository);
        //    Tender tender = service.GetById(2);
        //    TenderOffer offer = service.CreateTenderOffer(tender);
        //    Assert.True(offer.OfferItems.Count == 0);
        //}
    }
}
