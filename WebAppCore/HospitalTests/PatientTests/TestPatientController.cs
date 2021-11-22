using Xunit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hospital_API.Controllers;
using Microsoft.EntityFrameworkCore;
using Hospital.SharedModel;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalTests.PatientTest
{
    public class TestPatientController
    {

        public IPatientRepository inMemoryRepo;

        public TestPatientController() {

        }

        private IPatientRepository GetInMemoryPersonRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();
            return new PatientRepository(hospitalContext);
        }

        [Fact]
        public void Test_token_not_found()
        {

            //Arrange
            inMemoryRepo = GetInMemoryPersonRepository();
            Patient patient = new Patient()
            {
                Id = 1,
                UserName = "mihajlo",
                Password = "123",
                EMail = "ja@ja.com",
                Token = "1234567",
                Activated = false
            };
            //Act
            inMemoryRepo.AddPatient(patient);
            var controller = new PatientsController();

            controller._patientService = new PatientService(inMemoryRepo);
            var response =(NotFoundResult) controller.ActivatePatient("123456722");

            //Assert
            Assert.Equal(404,response.StatusCode);
            //Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public void Test_token_found()
        {
            //Arrange
            inMemoryRepo = GetInMemoryPersonRepository();
            Patient patient = new Patient()
            {
                Id = 2,
                UserName = "pera",
                Password = "123",
                EMail = "ja@ja.com",
                Token = "dadada",
                Activated = false
            };
            //Act
            inMemoryRepo.AddPatient(patient);

            var controller = new PatientsController();

            controller._patientService = new PatientService(inMemoryRepo);
            var response = (RedirectResult)controller.ActivatePatient("dadada");

            //Assert
            Assert.Equal("http://localhost:4200/login", response.Url);
            //Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void Test_patient_not_found()
        {

            //Arrange
            inMemoryRepo = GetInMemoryPersonRepository();
            var controller = new PatientsController();

            controller._patientService = new PatientService(inMemoryRepo);
            var response = controller.GetPatient(50);

            //Assert
            Assert.Null(response.Value);
            //Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public void Test_patient_found()
        {
            //Arrange
            inMemoryRepo = GetInMemoryPersonRepository();
            Patient patient = new Patient()
            {
                Id = 3,
                UserName = "perislav",
                Password = "123perislav",
                EMail = "perislav.com",
                Token = "dad554a",
                Activated = false
            };
            //Act
            inMemoryRepo.AddPatient(patient);

            var controller = new PatientsController();

            controller._patientService = new PatientService(inMemoryRepo);
            var response = controller.GetPatient(3);

            //Assert
            Assert.Equal(3, response.Value.Id);
            //Assert.Equal(404, result.StatusCode);
        }
    }
}
