using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Services;
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
        public void OrderingUnexistingMedicinesTest()
        {
            var stubRepository = new Mock<IMedicinesRepository>();
            service = new MedicineService(stubRepository.Object, new MedicalRecordsRepository(), new ExaminationRepository());
            List<Medicine> medicines = new List<Medicine>();
            Medicine medicine = new Medicine("1", "Andol", 200, 2);
            stubRepository.Setup(m => m.GetAll()).Returns(medicines);
            stubRepository.Setup(m => m.Add(medicine)).Callback((Medicine m) => medicines.Add(m));

            service.AddOrderedMedicine(medicine);

            Assert.Single(medicines);
        }
        [Fact]
        public void OrderingExistingMedicinesTest()
        {
            var stubRepository = new Mock<IMedicinesRepository>();
            service = new MedicineService(stubRepository.Object, new MedicalRecordsRepository(), new ExaminationRepository());
            List<Medicine> medicines = new List<Medicine>();
            Medicine medicine = new Medicine("1", "Andol", 200, 2);
            medicines.Add(medicine);
            stubRepository.Setup(m => m.GetAll()).Returns(medicines);

            service.AddOrderedMedicine(medicine);

            Assert.Equal(4, medicines[0].Quantity);
        }
    }
}
