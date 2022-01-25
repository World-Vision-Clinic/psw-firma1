using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.Schedule.Repository;
using Hospital.SharedModel;
using Hospital_API.DTO;
using Hospital_API.Mapper;
using Hspital_API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Controllers
{
    [Route("api/Event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        public EventService _eventService;
        HospitalContext _context;
        EventsDbContext _eventsContext;
        IAppointmentRepository appointmentRepository;
        public PatientService _patientService;
        public bool test = false;

        public EventController()
        {
            _context = new HospitalContext();
            _eventsContext = new EventsDbContext();
            _eventService = new EventService(new EventRepository(_eventsContext));
            appointmentRepository = new AppointmentRepository(_context);
            _patientService = new PatientService(new PatientRepository(_context), appointmentRepository);

        }

        [Authorize(Roles = "Manager")]
        [HttpGet("statistics")]
        public ActionResult<IEnumerable<EventStatisticDTO>> GetEventStatistics()
        {
            return Ok(EventStatisticMapper.GetAllEventStatistics());
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult Save(Event newEvent)
        {
            Patient patient = getCurrentPatient();
            newEvent.EventTime = DateTime.Now;
            newEvent.PatientAge = newEvent.EventTime.Year - patient.DateOfBirth.Year;
            _eventService.Save(newEvent);
            return Ok();
        }
        private Patient getCurrentPatient()
        {
            if (test)
            {
                Patient patient = _patientService.FindByUserName("Marko123");
                return patient;
            }
            else
            {
                string username = User.FindFirst("username")?.Value;
                Patient patient = _patientService.FindByUserName(username);
                return patient;
            }
        }
    }
}
