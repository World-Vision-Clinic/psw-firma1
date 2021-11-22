using Moq;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Repository.RepositoryInterfaces;
using Pharmacy.Service;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PharmacyApplicationTests
{
    public class MedicineInformationTests
    {
        [Fact]
        public void Find_specific_medicine()
        {
            IMedicineRepository stubRepository = CreateStubRepository();

            MedicineService service = new MedicineService(stubRepository);
            Medicine medicine = service.GetById(1L);

            Assert.NotNull(medicine);
        }

        private static IMedicineRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            var medicines = new List<Medicine>();

            Medicine brufen = new Medicine(1L, "Brufen", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 500.00, "mainPrecautions1", "potentialDangers1", new List<Substance>(), 200);
            Medicine panadol = new Medicine(2L, "Panadol", "Galenika", "sideEffects2", "usage2", new List<SubstituteMedicine>(), 400.00, "mainPrecautions2", "potentialDangers2", new List<Substance>(), 500);
            Medicine andol = new Medicine(3L, "Andol", "Galenika", "sideEffects3", "usage3", new List<SubstituteMedicine>(), 400.00, "mainPrecautions3", "potentialDangers3", new List<Substance>(), 300);

            SubstituteMedicine substituteMedicine = new SubstituteMedicine();
            substituteMedicine.Medicine = brufen;
            substituteMedicine.MedicineId = brufen.MedicineId;
            substituteMedicine.Substitute = panadol;
            substituteMedicine.SubstituteId = panadol.MedicineId;

            brufen.SubstituteMedicines.Add(substituteMedicine);

            medicines.Add(brufen);
            medicines.Add(panadol);
            medicines.Add(andol);

            stubRepository.Setup(m => m.GetAll()).Returns(medicines);
            return stubRepository.Object;
        }
        
       
    }
}
