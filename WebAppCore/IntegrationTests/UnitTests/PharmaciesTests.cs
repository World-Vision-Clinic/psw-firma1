using Integration.Pharmacy.Model;
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
            PharmaciesService service = new PharmaciesService();
            List<PharmacyProfile> pharmacies = service.GetAll();
            Assert.NotNull(pharmacies);
        }
    }
}
