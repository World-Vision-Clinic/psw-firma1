using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PharmacyApplicationTests.IntegrationTests
{
    public class ProcurementOfDrugsTests
    {
        public void Procure_medicine_true()
        {

            MedicineService service = new MedicineService(new MedicineRepository());
            List<Medicine> medicines = new List<Medicine>();

            Assert.True(service.ProcureMedicine(15215, 1));

        }
    }
}
