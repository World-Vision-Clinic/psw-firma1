using Moq;
using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using Pharmacy.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PharmacyApplicationTests.UnitTests
{
    public class SarchTest
    {
        MedicineService service;
        SubstanceService substanceService;

        [Fact]
        public void Search_medicine_by_name()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            service = new MedicineService(stubRepository.Object);
            List<Medicine> medicines = new List<Medicine>();
            Medicine medicine = new Medicine(55, "Fervex", "", "", "", new List<SubstituteMedicine>(), 250, "", "", new List<Substance>(), 213);
            medicines.Add(medicine);
            stubRepository.Setup(m => m.GetAll()).Returns(medicines);

            List<Medicine> ret = service.GetByName("Fervex");

            Assert.Contains(ret, item => item.MedicineId == 55);
        }

    }
}
