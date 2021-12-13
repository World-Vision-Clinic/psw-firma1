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
using Hospital.Schedule.Repository;
using Hospital.Schedule.Model;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestPatientController
    {
        public PatientRepository _patientRepository;
        public PatientAllergenRepository _patientAllergenRepository;
        public AllergenRepository _allergenRepository;
        public DoctorRepository _doctorRepository;
        public AppointmentRepository _appointmentRepository;

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
            _appointmentRepository = new AppointmentRepository(hospitalContext);

            _patientsController = new PatientsController();
            _patientsController._patientService = new PatientService(_patientRepository, _appointmentRepository);
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
            Doctor doctor = new Doctor()
            {
                Id = 10,
                FirstName = "MarkovDoktorIme",
                LastName = "MarkovDoktorPrezime"
            };
            _patientRepository.AddPatient(patient);
            _doctorRepository.AddDoctor(doctor);

            var response = _patientsController.GetPatient(4);

            Assert.Equal("MarkovDoktorIme MarkovDoktorPrezime", response.Value.PreferedDoctorName);
        }

        [Fact]
        public void Test_block_patient_blockable()
        {
            Patient patient = new Patient()
            {
                Id = 100,
                UserName = "branko1",
                Password = "branko123",
                EMail = "brankobrankovic@gmail.com",
                PreferedDoctor = 0,
                Token = "branko123",
                Activated = true,
                IsBlocked = false
            };
            _patientRepository.AddPatient(patient);

            Appointment appointment1 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-15),
                Id = 100,
                PatientForeignKey = 100,
                DoctorForeignKey = 0,
                IsCanceled = true,
                Type = AppointmentType.Appointment
            };

            Appointment appointment2 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-7),
                Id = 101,
                PatientForeignKey = 100,
                DoctorForeignKey = 0,
                IsCanceled = true,
                Type = AppointmentType.Appointment
            };

            Appointment appointment3 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-28),
                Id = 102,
                PatientForeignKey = 100,
                DoctorForeignKey = 0,
                IsCanceled = true,
                Type = AppointmentType.Appointment
            };
            _appointmentRepository.AddAppointment(appointment1);
            _appointmentRepository.AddAppointment(appointment2);
            _appointmentRepository.AddAppointment(appointment3);

            var response = (OkResult) _patientsController.BlockPatient("branko1");
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public void Test_block_patient_unblockable() //TODO: Reformat sve ovo
        {
            Patient patient = new Patient()
            {
                Id = 101,
                UserName = "sima1",
                Password = "sima123",
                EMail = "simasimic@gmail.com",
                PreferedDoctor = 0,
                Token = "sima123",
                Activated = true,
                IsBlocked = false
            };
            _patientRepository.AddPatient(patient);

            Appointment appointment1 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-15),
                Id = 200,
                PatientForeignKey = 101,
                DoctorForeignKey = 0,
                IsCanceled = true,
                Type = AppointmentType.Appointment
            };

            Appointment appointment2 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-7),
                Id = 201,
                PatientForeignKey = 101,
                DoctorForeignKey = 0,
                IsCanceled = false,
                Type = AppointmentType.Appointment
            };

            Appointment appointment3 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-28),
                Id = 202,
                PatientForeignKey = 101,
                DoctorForeignKey = 0,
                IsCanceled = true,
                Type = AppointmentType.Appointment
            };
            _appointmentRepository.AddAppointment(appointment1);
            _appointmentRepository.AddAppointment(appointment2);
            _appointmentRepository.AddAppointment(appointment3);

            var response = (BadRequestResult)_patientsController.BlockPatient("sima1");
            Assert.Equal(400, response.StatusCode);
        }
    }
}
