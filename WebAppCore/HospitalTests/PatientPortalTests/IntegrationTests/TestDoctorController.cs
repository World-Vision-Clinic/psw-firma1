using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.SharedModel;
using Hospital_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestDoctorController
    {
        public PatientRepository _patientRepository;
        public PatientAllergenRepository _patientAllergenRepository;
        public AllergenRepository _allergenRepository;
        public DoctorRepository _doctorRepository;

        public DoctorsController _doctorsController;

        public TestDoctorController()
        {
            GetInMemoryPersonRepository();
        }

        private void GetInMemoryPersonRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureCreated();
            _patientRepository = new PatientRepository(hospitalContext);
            _allergenRepository = new AllergenRepository(hospitalContext);
            _patientAllergenRepository = new PatientAllergenRepository(hospitalContext, _patientRepository, _allergenRepository);
            _doctorRepository = new DoctorRepository(hospitalContext, _patientRepository);

            _doctorsController = new DoctorsController(hospitalContext, new DoctorService(_doctorRepository));
        }

        [Fact]
        public void Test_doctor_not_found()
        {
            var response = _doctorsController.GetDoctor(50);

            Assert.Null(response.Value);

        }

        [Fact]
        public void Test_doctor_found()
        {
            Doctor doctor = new Doctor()
            {
                Id = 1,
                FirstName = "TestDoktorIme",
                LastName = "TestDoktorPrezime"
            };

            _doctorRepository.AddDoctor(doctor);
            var response = _doctorsController.GetDoctor(1);

            Assert.Equal("TestDoktorIme", response.Value.FirstName);
            Assert.Equal("TestDoktorPrezime", response.Value.LastName);
        }

        [Fact]
        public void Test_get_docotrs_for_specialty()
        {
            Doctor doctor1 = new Doctor()
            {
                Id = 2,
                FirstName = "TestDoktorIme",
                LastName = "TestDoktorPrezime",
                Type = DoctorType.Ophthalmologist
            };

            Doctor doctor2 = new Doctor()
            {
                Id = 3,
                FirstName = "TestDoktorIme",
                LastName = "TestDoktorPrezime",
                Type = DoctorType.Ophthalmologist

            };

            Doctor doctor3 = new Doctor()
            {
                Id = 4,
                FirstName = "TestDoktorIme",
                LastName = "TestDoktorPrezime",
                Type = DoctorType.Radiologist
            };

            _doctorRepository.AddDoctor(doctor1);
            _doctorRepository.AddDoctor(doctor2);
            _doctorRepository.AddDoctor(doctor3);
            var response = _doctorsController.GetDoctorForSpecialty(2);
            var result = response.Result as OkObjectResult;
            var data = result.Value as List<Doctor>;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Single(data);
            Assert.NotNull(response);
            Assert.Contains(doctor3, data);
        }
    }
}
