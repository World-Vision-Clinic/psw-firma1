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
        public void GettingAllPharmaciesTest()
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
        public void FilteringByExistingCityTest()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            PharmacyProfile pp1 = new PharmacyProfile("Benu", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Beograd");
            PharmacyProfile pp2 = new PharmacyProfile("Jankovic", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Nevesinje");

            pharmacies.Add(pp1);
            pharmacies.Add(pp2);

            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);
            service = new PharmaciesService(stubRepository.Object);
            
            List<PharmacyProfile> foundedPharmacies = service.GetFiltered("Beograd");
            
            Assert.NotEmpty(foundedPharmacies);
        }
        [Fact]
        public void FilteringByUnexistingCityTest()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            PharmacyProfile pp1 = new PharmacyProfile("Benu", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Beograd");
            PharmacyProfile pp2 = new PharmacyProfile("Jankovic", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Nevesinje");

            pharmacies.Add(pp1);
            pharmacies.Add(pp2);

            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);
            service = new PharmaciesService(stubRepository.Object);

            List<PharmacyProfile> foundedPharmacies = service.GetFiltered("Sarajevo");

            Assert.Empty(foundedPharmacies);
        }

        [Fact]
        public void FilteringByExistingAddressTest()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            PharmacyProfile pp1 = new PharmacyProfile("Benu", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Beograd");
            PharmacyProfile pp2 = new PharmacyProfile("Jankovic", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Nevesinje");

            pharmacies.Add(pp1);
            pharmacies.Add(pp2);

            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);
            service = new PharmaciesService(stubRepository.Object);

            List<PharmacyProfile> foundedPharmacies = service.GetFiltered("Nemanjica");

            Assert.NotEmpty(foundedPharmacies);
        }
        [Fact]
        public void FilteringByUnexistingAddressTest()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            PharmacyProfile pp1 = new PharmacyProfile("Benu", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Beograd");
            PharmacyProfile pp2 = new PharmacyProfile("Jankovic", "dsadkasj", "localhost:8080", ProtocolType.GRPC, "Nemanjica", "Nevesinje");

            pharmacies.Add(pp1);
            pharmacies.Add(pp2);

            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);
            service = new PharmaciesService(stubRepository.Object);

            List<PharmacyProfile> foundedPharmacies = service.GetFiltered("Kralja Petra");

            Assert.Empty(foundedPharmacies);
        }
    }
}
