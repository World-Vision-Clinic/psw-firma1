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

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestPatientController
    {
        public PatientRepository _patientRepository;
        public PatientAllergenRepository _patientAllergenRepository;
        public AllergenRepository _allergenRepository;
        public DoctorRepository _doctorRepository;

        public PatientsController _patientsController;

        public TestPatientController() {
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

            _patientsController = new PatientsController();
            _patientsController._patientService = new PatientService(_patientRepository);
            _patientsController._allergenService = new AllergenService(_allergenRepository);
            _patientsController._patientAllergenService = new PatientAllergenService(_patientAllergenRepository);
            _patientsController._doctorService = new DoctorService(_doctorRepository);
        }

        [Fact]
        public void Test_token_not_found()
        {
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
            _patientRepository.AddPatient(patient);

            var response =(NotFoundResult) _patientsController.ActivatePatient("123456722");

            //Assert
            Assert.Equal(404,response.StatusCode);
            //Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public void Test_token_found()
        {
            Patient patient = new Patient()
            {
                Id = 2,
                UserName = "pera",
                Password = "123",
                EMail = "ja@ja.com",
                Token = "dadada",
                Activated = false
            };
            _patientRepository.AddPatient(patient);

            var response = (RedirectResult)_patientsController.ActivatePatient("dadada");

            //Assert
            Assert.Equal("http://localhost:4200/login", response.Url);
            //Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void Test_patient_not_found()
        {
            var response = _patientsController.GetPatient(50);

            //Assert
            Assert.Null(response.Value);
            //Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public void Test_patient_found()
        {
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
            _patientRepository.AddPatient(patient);

            var response = _patientsController.GetPatient(3);

            //Assert
            Assert.Equal("perislav", response.Value.UserName);
            //Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void Test_preferred_doctor_found()
        {
            Patient patient = new Patient()
            {
                Id = 4,
                UserName = "Marko",
                Password = "markomarko",
                EMail = "markomarko@gmail.com",
                PreferedDoctor = 10,
                Token = "marko123",
                Activated = true
            };
            Doctor doctor = new Doctor(10, "MarkovDoktorIme", "MarkovDoktorPrezime");
            _patientRepository.AddPatient(patient);
            _doctorRepository.AddDoctor(doctor);

            var response = _patientsController.GetPatient(4);

            Assert.Equal("MarkovDoktorIme MarkovDoktorPrezime", response.Value.PreferedDoctorName);
        }
    }
}
