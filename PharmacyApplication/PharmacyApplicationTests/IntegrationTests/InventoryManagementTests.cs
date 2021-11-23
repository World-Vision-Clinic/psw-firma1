using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PharmacyApplicationTests.IntegrationTests
{
    public class InventoryManagementTests
    {

        [Fact]
        public void Add_medicine_true()
        {

            MedicineService service = new MedicineService(new MedicineRepository());

            Medicine strepsils = new Medicine(197, "Strepsils", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 200.00, "mainPrecautions1", "potentialDangers1", null, 100);


            Assert.True(service.AddMedicine(strepsils));
        }

       /* [Fact]
        public void Add_medicine_false()
        {
            MedicineService service = new MedicineService(new MedicineRepository());

            Medicine brufen = new Medicine(151521554, "Brufen", "Galenika", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 200.00, "mainPrecautions1", "potentialDangers1", null, 100);

            Assert.False(service.AddMedicine(brufen));

        }*/


        [Fact]
        public void Delete_medicine_false()
        {
            MedicineService service = new MedicineService(new MedicineRepository());


            Assert.False(service.DeleteMedicine(50));

        }

        [Fact]
        public void Update_medicine_true()
        {
            MedicineService service = new MedicineService(new MedicineRepository());
            Medicine brufen = new Medicine(151521554, "Brufen", "Galenika 22", "sideEffects1", "usage1", new List<SubstituteMedicine>(), 200.00, "mainPrecautions1", "potentialDangers1", null, 100);


            Assert.True(service.UpdateMedicine(brufen));

        }


        /*[Fact]
        public void Delete_medicine_true()
        {
            MedicineService service = new MedicineService(new MedicineRepository());

            Assert.True(service.DeleteMedicine(1));

        }*/


    }
}
