using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Service;
using Integration_API.Controller;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.IntegrationTests
{
    public class PharmaciesFilteringTest
    {
        PharmaciesService service;
        
        [Fact]
        public void FilteringPharmacyByCityTest()
        {
            PharmaciesController pc = new PharmaciesController();

            var pharmacyFound = pc.Get("Novi Sad");

            Assert.IsType<OkObjectResult>(pharmacyFound);
        }

        [Fact]
        public void FilteringPharmacyByAdressTest()
        {
            PharmaciesController pc = new PharmaciesController();

            var pharmacyFound = pc.Get("Nemanjica bb");

            Assert.IsType<OkObjectResult>(pharmacyFound);
        }

    }
}
