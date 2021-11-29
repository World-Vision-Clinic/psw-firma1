using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestAppointmentController
    {
        public IAppointmentRepository inMemoryRepo;

        public TestAppointmentController()
        {

        }

        private IAppointmentRepository GetInMemoryPersonRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();
            return new AppointmentRepository(hospitalContext);
        }

        [Fact]
        public void Test_appointment_by_patient_not_found()
        {

            //Arrange
            inMemoryRepo = GetInMemoryPersonRepository();
            var controller = new AppointmentController();

            controller._appointmentService = new AppointmentService(inMemoryRepo);
            var response = controller.GetAppointmentsByPatientId(50);

            //Assert
            Assert.Empty(response.Value.ToList());

        }

        [Fact]
        public void Test_appointment_by_patient_found()
        {
            //Arrange
            inMemoryRepo = GetInMemoryPersonRepository();
            Appointment appointment = new Appointment()
            {
                Id = 1,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = DateTime.Now,
                Time = TimeSpan.Zero
            };
            //Act
            inMemoryRepo.AddAppointment(appointment);

            var controller = new AppointmentController();

            controller._appointmentService = new AppointmentService(inMemoryRepo);
            var response = controller.GetAppointmentsByPatientId(1);

            //Assert
            Assert.Equal(1, response.Value.First().Id);
        }
    }
}
