using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API.Controllers;
using Hospital_API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestLoginController
    {
        public PatientRepository _patientRepository;
        public ManagerRepository _managerRepository;
        public AppointmentRepository _appointmentRepository;

        public LoginController _loginController;
        private void GetInMemoryPersonRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();
            _patientRepository = new PatientRepository(hospitalContext);
            _managerRepository = new ManagerRepository(hospitalContext);

            _loginController = new LoginController();
            _appointmentRepository = new AppointmentRepository(hospitalContext);
            _loginController._patientService = new PatientService(_patientRepository, _appointmentRepository);
            _loginController._managerService = new ManagerService(_managerRepository);
            _loginController.test = true;
        }
        public TestLoginController()
        {
            GetInMemoryPersonRepository();
        }

        [Theory]
        [InlineData("nekitamo", "1234", 401)]    // bad password
        [InlineData("nekitamo", "123", 200)]     // ok
        public void Test_user_doesnt_exist(string username, string password, int expectedStatusCode)
        {
            Manager manager = new Manager()
            {
                Id = 1,
                UserName = "nekitamo",
                Password = "123"
            };
            _managerRepository.AddManager(manager);

            var response = _loginController.Login(new LoginDTO(username, password));

            response.Equals(expectedStatusCode);
        }
    }
}
