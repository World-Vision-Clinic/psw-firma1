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
            Patient patient = new Patient(1, "mihajlo", "123", new FullName("Mihajlo", "Mihajlovic"), "ja@ja.com", false, Gender.Male, "1234567",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063111111", 0, 80, 180, BloodType.A, false, new List<Appointment>());
            _patientRepository.AddPatient(patient);

            var response = (NotFoundResult) _patientsController.ActivatePatient(Patient.TokenizeSHA256("mihajlo"));
            Assert.Equal(404,response.StatusCode);

        }

        [Fact]
        public void Test_token_found()
        {
            Patient patient = new Patient(2, "petar", "123", new FullName("Petar", "Petrovic"), "pera@gmail.com", false, Gender.Male, "1237567",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 0, 80, 180, BloodType.A, false, new List<Appointment>());
            _patientRepository.AddPatient(patient);

            var response = (RedirectResult)_patientsController.ActivatePatient("petar");
            Assert.Equal("http://localhost:4200/login", response.Url);
        }

        [Fact]
        public void Test_patient_not_found()
        {
            /*var response = _patientsController.GetPatient(50);
            Assert.Null(response.Value);*/
        }

        [Fact]
        public void Test_patient_found()
        {
            Patient patient = new Patient(3, "perislav", "123", new FullName("Pera", "Peric"), "perislav@gmail.com", false, Gender.Male, "1637567",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 0, 80, 180, BloodType.A, false, new List<Appointment>());
            _patientRepository.AddPatient(patient);
            /*var response = _patientsController.GetPatient(3);
            Assert.Equal("perislav", response.Value.UserName);*/
        }

        [Fact]
        public void Test_preferred_doctor_found()
        {
            Doctor doctor = new Doctor()
            {
                Id = 10,
                FirstName = "MarkovDoktorIme",
                LastName = "MarkovDoktorPrezime"
            };
            _doctorRepository.AddDoctor(doctor);

            var response = _patientsController.GetPatient();

            Assert.Equal("MarkovDoktorIme MarkovDoktorPrezime", response.Value.PreferedDoctorName);
        }

        [Fact]
        public void Test_block_patient_blockable()
        {
            Patient patient = new Patient(100, "branko1", "123", new FullName("Branko", "Brankovic"), "brankobrankovic@gmail.com", true, Gender.Male, "1677597",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 10, 80, 180, BloodType.A, false, new List<Appointment>());
            _patientRepository.AddPatient(patient);

            Appointment appointment1 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-15),
                Id = 100,
                PatientForeignKey = 100,
                DoctorForeignKey = 0,
                IsCancelled = true,
                Type = AppointmentType.Appointment
            };

            Appointment appointment2 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-7),
                Id = 101,
                PatientForeignKey = 100,
                DoctorForeignKey = 0,
                IsCancelled = true,
                Type = AppointmentType.Appointment
            };

            Appointment appointment3 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-28),
                Id = 102,
                PatientForeignKey = 100,
                DoctorForeignKey = 0,
                IsCancelled = true,
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
            Patient patient = new Patient(101, "sima1", "sima123", new FullName("Sima", "Simovic"), "simasimic@gmail.com", true, Gender.Male, "2677597",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 10, 80, 180, BloodType.A, false, new List<Appointment>());
            _patientRepository.AddPatient(patient);

            Appointment appointment1 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-15),
                Id = 200,
                PatientForeignKey = 101,
                DoctorForeignKey = 0,
                IsCancelled = true,
                Type = AppointmentType.Appointment
            };

            Appointment appointment2 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-7),
                Id = 201,
                PatientForeignKey = 101,
                DoctorForeignKey = 0,
                IsCancelled = false,
                Type = AppointmentType.Appointment
            };

            Appointment appointment3 = new Appointment()
            {
                Date = DateTime.Now.AddDays(-28),
                Id = 202,
                PatientForeignKey = 101,
                DoctorForeignKey = 0,
                IsCancelled = true,
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
