using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Integration;
using Integration.Pharmacy.Repository;
using Integration_API.Controller;
using Integration_API.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.IntegrationTests
{
    public class OrderingMedicinesTest
    {

        /*[Fact]
        public void OrderingUnexistingMedicineHospitalTest()
        {
            OrderedMedicineDTO omd = new OrderedMedicineDTO("Amoksicilin", "Zdravko", "none", "2 times a day", "200", "none", "none", "2", null, 200);
            MedicinesRepository mr = new MedicinesRepository();
            int oldCount = mr.GetAll().Count;
            MedicinesController pc = new MedicinesController(new PharmacyHTTPConnection());

            pc.Ordered(omd);

            Assert.Equal(mr.GetAll().Count, oldCount + 1);
        }*/

        [Fact]
        public void OrderingExistingMedicinesTest()
        {
            OrderedMedicineDTO omd = new OrderedMedicineDTO("Brufen", "Zdravko", "none", "2 times a day", "200", "none", "none", "2", null, 200);
            MedicinesRepository mr = new MedicinesRepository();
            double quantity = 0;
            foreach (Medicine med in mr.GetAll())
            {
                if (med.Name.Equals(omd.MedicineName))
                {
                    quantity = med.Quantity;
                }
            }
            MedicinesController pc = new MedicinesController(new PharmacyHTTPConnection());

            pc.Ordered(omd);


            MedicinesRepository mr1 = new MedicinesRepository();
            double newQuantity = 0;
            foreach (Medicine med in mr1.GetAll())
            {
                if (med.Name.Equals(omd.MedicineName))
                {
                    newQuantity = med.Quantity;
                }
            }

            Assert.Equal(quantity + int.Parse(omd.Quantity), newQuantity);
        }

        [Fact]
        public void CheckIfMedicineIsOrdered()
        {
            OrderingMedicineDTO omd = new OrderingMedicineDTO("http://localhost:34616", "Brufen", "100", "2");
            MedicinesController mc = new MedicinesController(new PharmacyHTTPConnection());
            
            bool requestOk = mc.SendMedicineOrderingRequest(omd, true);

            Assert.True(requestOk);
        }
    }
}
