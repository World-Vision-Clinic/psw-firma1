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
        //[Fact]
        //public void Search_medicine_by_substance()
        //{
        //    var stubRepositoryM = new Mock<IMedicineRepository>();
        //    var stubRepositoryS = new Mock<ISubstanceRepository>();
        //    service = new MedicineService(stubRepositoryM.Object);
        //    substanceService = new SubstanceService(stubRepository.Object);
        //    List<Medicine> medicines = new List<Medicine>(); 
        //    List<Substance> substances = new List<Substance>();
        //    Medicine medicine = new Medicine(55, "Fervex", "", "", "", new List<SubstituteMedicine>(), 250, "", "", substances, 213);
        //    Substance sub = new Substance(33, "s1", 213, 55);
        //    substances.Add(sub);
        //    stubRepositoryM.Setup(s => s.GetAll()).Returns(substances);
        //    medicine.Substances.Add(s);
        //    medicines.Add(medicine);
        //    stubRepositoryM.Setup(m => m.GetAll()).Returns(medicines);

        //    List<long> ret = substanceService.GetMedicineIdsBySubstance(33);
        //    List<Medicine> retMed = service.ConvertIdsToMedicines(ret);

        //    Assert.Contains(retMed, item => item.MedicineId == 55);
        //}
    }
}
