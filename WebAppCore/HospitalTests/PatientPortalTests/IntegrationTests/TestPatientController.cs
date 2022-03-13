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
using Shouldly;

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
            _patientsController.test = true;
        }

        [Fact]
        public void Test_token_not_found()
        {
            Patient patient = new Patient(1, "mihajlo", "123", new FullName("Mihajlo", "Mihajlovic"), "ja@ja.com", false, Gender.Male, "1234567",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063111111", 0, 80, 180, BloodType.A, false, new List<Appointment>(), " ");
            _patientRepository.AddPatient(patient);

            var response = (NotFoundResult)_patientsController.ActivatePatient("mihajlo");
            Assert.Equal(404, response.StatusCode);

        }

        [Fact]
        public void Test_token_found()
        {
            Patient patient = new Patient(2, "petar", "123", new FullName("Petar", "Petrovic"), "pera@gmail.com", false, Gender.Male, "1237567",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 0, 80, 180, BloodType.A, false, new List<Appointment>(), " ");
            _patientRepository.AddPatient(patient);

            var response = (RedirectResult)_patientsController.ActivatePatient("9d6245d7fb961b620b28c91d22e1a585413743388dc1833e678129f1751d94ae");
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
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 0, 80, 180, BloodType.A, false, new List<Appointment>(), " ");
            _patientRepository.AddPatient(patient);
            /*var response = _patientsController.GetPatient(3);
            Assert.Equal("perislav", response.Value.UserName);*/
        }

        [Fact]
        public void Test_preferred_doctor_found()
        {
            /*Doctor doctor = new Doctor()
            {
                Id = 10,
                FirstName = "MarkovDoktorIme",
                LastName = "MarkovDoktorPrezime"
            };
            _doctorRepository.AddDoctor(doctor);

            var response = _patientsController.GetPatient();

            Assert.Equal("MarkovDoktorIme MarkovDoktorPrezime", response.Value.PreferedDoctorName);*/
        }

        
        [Theory]
        [InlineData(101, "sima1", new bool[] {true, true, true}, new int[] { 1, 2, 3 }, HttpStatusCode.OK)]            // blocabale
        [InlineData(102, "sima2", new bool[] {true, false, true}, new int[] { 4, 5, 6 }, HttpStatusCode.BadRequest)]   // unblocabale
        public void Test_block_patient(int patientId, string username, bool[] isCanceled, int[] appointmentIds, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            Patient patient = new Patient(patientId, username, "sima123", new FullName("Sima", "Simovic"), "simasimic@gmail.com", true, Gender.Male, "2677597",
                DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 10, 80, 180, BloodType.A, false, new List<Appointment>(), " ");
            _patientRepository.AddPatient(patient);

            int[] days = { -15, -7, -28 };
            for(int i = 0; i < days.Length; i++)
            {
                Appointment appointment = new Appointment()
                {
                    Date = DateTime.Now.AddDays(days[i]),
                    Id = appointmentIds[i],
                    PatientForeignKey = patientId,
                    DoctorForeignKey = 0,
                    IsCancelled = isCanceled[i],
                    Type = AppointmentType.Appointment
                };
                _appointmentRepository.AddAppointment(appointment);
            }

            // Act
            HttpResponseMessage response = _patientsController.BlockPatient(username);
            
            // Assert
            response.StatusCode.ShouldBe(expectedStatusCode);
        }
    }
}
