using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.UnitTests
{
    public class PharmaciesFilteringTest
    {
        PharmaciesService service;
        [Fact]
        public void Test1()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            PharmacyProfile pp1 = new PharmacyProfile("Benu", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Beograd");
            PharmacyProfile pp2 = new PharmacyProfile("Jankovic", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Nevesinje");

            pharmacies.Add(pp1);
            pharmacies.Add(pp2);

            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);

            service = new PharmaciesService(stubRepository.Object);
            List<PharmacyProfile> allPharmacies = service.GetAll();
            Assert.Equal(2, allPharmacies.Count);
        }

        [Fact]
        public void FilteringCityTest()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            PharmacyProfile pp1 = new PharmacyProfile("Benu", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Beograd");
            PharmacyProfile pp2 = new PharmacyProfile("Jankovic", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Nevesinje");
            pharmacies.Add(pp1);
            pharmacies.Add(pp2);
            string city = "Nevesinje";
            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);
            service = new PharmaciesService(stubRepository.Object);

            List<PharmacyProfile> founded = service.FilterPharmacies(city);

            Assert.Single(founded);
        }
        [Fact]
        public void FilteringAddressTest()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            PharmacyProfile pp1 = new PharmacyProfile("Benu", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Bla bla", "Beograd");
            PharmacyProfile pp2 = new PharmacyProfile("Jankovic", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Nevesinje");
            pharmacies.Add(pp1);
            pharmacies.Add(pp2);
            string city = "Nemanjica";
            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);
            service = new PharmaciesService(stubRepository.Object);

            List<PharmacyProfile> founded = service.FilterPharmacies(city);

            Assert.Single(founded);
        }
    }
}
