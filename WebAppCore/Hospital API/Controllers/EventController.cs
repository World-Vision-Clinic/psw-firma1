using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
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
        EventService service = new EventService(new EventRepository());

        /*[HttpGet]
        public IActionResult GetAll()
        {
            ICollection<Event> events = service.GetAll();
            return Ok(events);
        }

        [HttpGet("{id?}")]
        public IActionResult GetById(int eventId)
        {
            Event ev = service.GetById(eventId);
            if (ev == null)
                return BadRequest("No event with that id");
            Event e = new Event(ev.Id, ev.Name, ev.EventTime);
            return Ok(e);
        }*/
        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult Save(Event newEvent)
        {
            newEvent.EventTime = DateTime.Now;
            service.Save(newEvent);
            return Ok();
        }
    }
}
