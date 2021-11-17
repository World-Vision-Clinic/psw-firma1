using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace PharmacyApplicationTests
{
    public class MedicineInformationTests
    {
        [Fact]
        public void Find_specific_medicine()
        {
            MedicineService service = new MedicineService(new MedicineRepository());
            Medicine medicine = service.GetById(1L);
            Assert.NotNull(medicine);
        }
    }
}
