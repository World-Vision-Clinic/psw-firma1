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
    public class MedicineSarchTest
    {

        private static IMedicineRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            var medicines = new List<Medicine>();

            Medicine brufen = new Medicine(1L, "Brufen", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 500.00, "mainPrecautions1", "potentialDangers1", new List<Substance>(), 200);
            Medicine panadol = new Medicine(2L, "Panadol", "Galenika", "sideEffects2", "usage2", new List<SubstituteMedicine>(), 400.00, "mainPrecautions2", "potentialDangers2", new List<Substance>(), 500);
            Medicine andol1 = new Medicine(3L, "Andol", "Galenika", "sideEffects3", "usage3", new List<SubstituteMedicine>(), 400.00, "mainPrecautions3", "potentialDangers3", new List<Substance>(), 300);
            Medicine andol2 = new Medicine(4L, "Andol", "Galenika", "sideEffects3", "usage3", new List<SubstituteMedicine>(), 200.00, "mainPrecautions3", "potentialDangers3", new List<Substance>(), 300);

            medicines.Add(brufen);
            medicines.Add(panadol);
            medicines.Add(andol1);
            medicines.Add(andol2);

            stubRepository.Setup(m => m.GetAll()).Returns(medicines);
            return stubRepository.Object;
        }

        [Fact]
        public void Search_medicine_by_name()
        {
            IMedicineRepository stubRepository = CreateStubRepository(); 
            MedicineService service = new MedicineService(stubRepository);
            
            List<Medicine> ret = service.GetByName("and"); 

            Assert.Contains(ret, item => item.MedicineId == 3L) ; 
        }

    }
}
