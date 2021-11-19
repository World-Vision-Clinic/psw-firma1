using Integration;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Service;
using Integration.Repositories.Interfaces;
using Integration.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.UnitTests
{
    public class OrderingMedicineTest
    {
        MedicineService service;

        [Fact]
        public void OrderingMedicinesTest()
        {
            var stubRepository = new Mock<IMedicinesRepository>();
            service = new MedicineService(stubRepository.Object, new MedicalRecordsRepository());
            List<Medicine> medicines = new List<Medicine>();
            Medicine medicine = new Medicine("1", "Andol", 200, 2);
            stubRepository.Setup(x => x.AddOrderedMedicine(medicine)).Callback((Medicine m) => medicines.Add(m));

            service.AddOrderedMedicine(medicine);

            Assert.NotEmpty(medicines);
        }
    }
}
