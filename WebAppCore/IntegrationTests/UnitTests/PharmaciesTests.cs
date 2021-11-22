using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTests
{
    public class PharmaciesTests
    {
        [Fact]
        public void Test1()
        {
            PharmaciesService service = new PharmaciesService(new PharmaciesRepository());
            List<PharmacyProfile> pharmacies = service.GetAll();
            Assert.NotNull(pharmacies);
        }
    }
}
