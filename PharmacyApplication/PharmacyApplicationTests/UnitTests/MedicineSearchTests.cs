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

        private static IMedicineRepository CreateStubRepositoryMedicine()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            var medicines = new List<Medicine>();
            var substances = new List<Substance>();

            Medicine brufen = new Medicine(1L, "Brufen", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 500.00, "mainPrecautions1", "potentialDangers1", new List<Substance>(), 200);
            Medicine panadol = new Medicine(2L, "Panadol", "Galenika", "sideEffects2", "usage2", new List<SubstituteMedicine>(), 400.00, "mainPrecautions2", "potentialDangers2", new List<Substance>(), 500);
            Medicine andol1 = new Medicine(3L, "Andol", "Galenika", "sideEffects3", "usage3", new List<SubstituteMedicine>(), 400.00, "mainPrecautions3", "potentialDangers3", new List<Substance>(), 300);
            Medicine andol2 = new Medicine(4L, "Andol", "Galenika", "sideEffects3", "usage3", new List<SubstituteMedicine>(), 200.00, "mainPrecautions3", "potentialDangers3", new List<Substance>(), 300);

            Substance substance = new Substance(1L, "s1", 100, brufen.MedicineId);
            substance.Medicine = brufen;
            brufen.Substances.Add(substance);
            substances.Add(substance);

            medicines.Add(brufen);
            medicines.Add(panadol);
            medicines.Add(andol1);
            medicines.Add(andol2);

            stubRepository.Setup(m => m.GetAll()).Returns(medicines);
            return stubRepository.Object;
        }

        private static ISubstanceRepository CreateStubRepositorySubstance()
        {
            var stubRepository = new Mock<ISubstanceRepository>();  
            var substances = new List<Substance>();

            Medicine brufen = new Medicine(1L, "Brufen", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 500.00, "mainPrecautions1", "potentialDangers1", new List<Substance>(), 200);
            Substance substance = new Substance(1L, "s1", 100, brufen.MedicineId);
            substance.Medicine = brufen; 
            brufen.Substances.Add(substance);
            substances.Add(substance);

            stubRepository.Setup(s => s.GetAll()).Returns(substances); 
            return stubRepository.Object;
        }

        [Fact]
        public void Search_medicine_by_name()
        {
            IMedicineRepository stubRepository = CreateStubRepositoryMedicine(); 
            MedicineService service = new MedicineService(stubRepository);
            
            List<Medicine> ret = service.GetByName("and"); 

            Assert.Contains(ret, item => item.MedicineId == 3L) ; 
        }

        [Fact]
        public void Search_medicine_by_substance()
        {
            IMedicineRepository stubRepositoryM = CreateStubRepositoryMedicine();
            ISubstanceRepository stubRepositoryS = CreateStubRepositorySubstance();
            MedicineService service = new MedicineService(stubRepositoryM);
            SubstanceService substanceService = new SubstanceService(stubRepositoryS);

            List<long> ret = substanceService.GetMedicineIdsBySubstance(1L);  
            List<Medicine> retMed = service.ConvertIdsToMedicines(ret);

            Assert.Contains(retMed, item => item.MedicineId == 1L);

        }

    }
}
