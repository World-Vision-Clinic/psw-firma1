using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Repositories.Interfaces;
using Moq;
using Integration;
using Integration.Pharmacy.Repository;
using Hospital.MedicalRecords.Services;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;

namespace IntegrationTests.UnitTests
{
    public class MedicineTest
    {

        [Fact]
        public void Find_cousumed_medicine()
        {

            MedicineService medicineService = new MedicineService(CreateMedicinesRepository(), CreateMedicalRecordsRepository(), CreateExaminationRepository());


            List<Medicine> foundMedicine = medicineService.GetConsumedMedicineInPeriod(new DateTime(2021, 11, 10), new DateTime(2021, 11, 12));


            Assert.NotEmpty(foundMedicine);
        }
        [Fact]
        public void Find_no_cousumed_medicine()
        {
            MedicineService medicineService = new MedicineService(CreateMedicinesRepository(), CreateMedicalRecordsRepository(), CreateExaminationRepository());


            List<Medicine> foundMedicine = medicineService.GetConsumedMedicineInPeriod(new DateTime(2021, 11, 15), new DateTime(2021, 11, 16));


            Assert.Empty(foundMedicine);
        }

        [Fact]  
        public void Create_consumed_file()
        {

            MedicineService medicineService = new MedicineService(CreateMedicinesRepository(), CreateMedicalRecordsRepository(), CreateExaminationRepository());
            
            medicineService.CreateConsumedMedicinesInPeriodFile(new DateTime(2021, 11, 15), new DateTime(2021, 11, 16));

            Assert.NotNull(System.IO.File.OpenRead("consumed-medicine.txt"));
        }

        private static IMedicinesRepository CreateMedicinesRepository()
        {
            var medicine = new Medicine();
            medicine.Name = "Brufen";
            medicine.Quantity = 1;
            medicine.ID = "1";


            var stubMedicinesRepository = new Mock<IMedicinesRepository>();
            stubMedicinesRepository.Setup(m => m.GetByID("1")).Returns(medicine);
            return stubMedicinesRepository.Object;

        }

        private static IExaminationRepository CreateExaminationRepository()
        {
            var medicine = new Medicine();
            medicine.Name = "Brufen";
            medicine.Quantity = 1;
            medicine.ID = "1";
            Therapy therapy = new Therapy();
            therapy.Id = 1;
            therapy.MedicineId = "1";
            List<Examination> examinations = new List<Examination>();
            var examination = new Examination();
            examination.Id = 1;
            examination.TherapyId = 1;
            examination.dateOfExamination = new DateTime(2021, 11, 11);

            examinations.Add(examination);
            var stubMedicinesRepository = new Mock<IExaminationRepository>();
            stubMedicinesRepository.Setup(m => m.GetTherapyById(1)).Returns(therapy);
            stubMedicinesRepository.Setup(m => m.GetAllByMedicalRecordId("1")).Returns(examinations);
            return stubMedicinesRepository.Object;

        }

        private static IMedicalRecordsRepository CreateMedicalRecordsRepository()
        {
            var stubMedicalRecordsRepository = new Mock<IMedicalRecordsRepository>();
            List<MedicalRecord> medicalRecords = new List<MedicalRecord>();
            var medicalRecord = new MedicalRecord();
            medicalRecord.MedicalRecordID = "1";
            medicalRecord.ParentName = "Pacijent1";
            var examination = new Examination();
            examination.dateOfExamination = new DateTime(2021, 11, 11);
            medicalRecord.AddExamination(examination);

            MedicineTherapy medicineTherapy = new MedicineTherapy();
            Medicine medicine = new Medicine();
            medicine.ID = "1";
            medicineTherapy.Medicine = medicine;
            examination.therapy.AddMedicine(medicineTherapy);
            medicalRecords.Add(medicalRecord);
            stubMedicalRecordsRepository.Setup(m => m.GetAll()).Returns(medicalRecords);
            return stubMedicalRecordsRepository.Object;
        }
    }
}
