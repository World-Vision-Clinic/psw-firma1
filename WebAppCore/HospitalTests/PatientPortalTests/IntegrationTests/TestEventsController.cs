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
using Hospital_API.DTO;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestEventsController
    {
        /*public PatientRepository _patientRepository;
        public AppointmentRepository _appointmentRepository;
        public EventRepository _eventsRepository;

        public EventController _eventController;

        private void GetInMemoryPersonRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureCreated();

            DbContextOptions<TestEventsDbContext> optionsEvents;
            var builderEvents = new DbContextOptionsBuilder<TestEventsDbContext>();
            builderEvents.UseInMemoryDatabase("TestEventsDb");
            optionsEvents = builderEvents.Options;
            TestEventsDbContext eventsContext = new TestEventsDbContext(optionsEvents);
            eventsContext.Database.EnsureCreated();

            _patientRepository = new PatientRepository(hospitalContext);
            _appointmentRepository = new AppointmentRepository(hospitalContext);
            _eventsRepository = new EventRepository(eventsContext);

            _eventController = new EventController();
            _eventController._patientService = new PatientService(_patientRepository, _appointmentRepository);
            _eventController._eventService = new EventService(_eventsRepository);
            _eventController.test = true;
        }

        public TestEventsController() 
        {
            GetInMemoryPersonRepository();
            DateTime date = DateTime.Now.AddDays(-20);
            Event event1 = new Event(1, "START", date, new TimeSpan(0,0,0), 20);
            Event event2 = new Event(2, "DATE-NEXT", date.AddSeconds(20), new TimeSpan(0,0,20), 20);
            Event event3 = new Event(3, "SPEC-NEXT", date.AddSeconds(30), new TimeSpan(0, 0, 10), 20);
            Event event4 = new Event(4, "DOC-NEXT", date.AddSeconds(50), new TimeSpan(0,0,30), 20);
            Event event5 = new Event(5, "END", date.AddSeconds(70), new TimeSpan(0,0,20), 20);

            _eventsRepository.Save(event1);
            _eventsRepository.Save(event2);
            _eventsRepository.Save(event3);
            _eventsRepository.Save(event4);
            _eventsRepository.Save(event5);
        }
       
        [Fact]
        public void Test_event_stats()
        {
            var response = _eventController.GetEventStatistics();
            var result = (OkObjectResult)response.Result;
            var data = (List<EventStatisticDTO>)result.Value;

            Assert.Equal(4, data.Count);
            Assert.Equal(100, data[0].Data[0].Value);
            Assert.Equal(20, data[1].Data[0].Value);
            Assert.Equal(10, data[1].Data[1].Value);
            Assert.Equal(30, data[1].Data[2].Value);
            Assert.Equal(20, data[1].Data[3].Value);
            Assert.Equal(200, result.StatusCode);
        }*/


    }
}
