using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Services;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HospitalTests.IntegratorsTests
{
    public class OrderingMedicineTest
    {
        MedicineService service;

        [Theory]
        [InlineData("1", "Andol", 200, 2, 1, 4)]    // ordering existing medicine
        [InlineData("2", "Aspirin", 300, 2, 2, 2)]  // ordering unexisting medicine
        public void OrderingMedicinesTest(string medicineId, string medicineName, double dosage, int quantity, int expectedCount, int expectedQuantity)
        {
            var stubRepository = new Mock<IMedicinesRepository>();
            service = new MedicineService(stubRepository.Object, new MedicalRecordsRepository(), new ExaminationRepository());
            List<Medicine> medicines = new List<Medicine>();
            Medicine existingMedicine = new Medicine("1", "Andol", 200, 2);
            medicines.Add(existingMedicine);
            stubRepository.Setup(m => m.GetAll()).Returns(medicines);
            Medicine orderedMedicine = new Medicine(medicineId, medicineName, dosage, quantity);
            stubRepository.Setup(m => m.Add(orderedMedicine)).Callback((Medicine m) => medicines.Add(m));

            service.AddOrderedMedicine(orderedMedicine);

            medicines.Count.ShouldBe(expectedCount);
            medicines.Where(m => m.Name == "Andol").SingleOrDefault().Quantity.ShouldBe(expectedQuantity);
        }

    }
}
