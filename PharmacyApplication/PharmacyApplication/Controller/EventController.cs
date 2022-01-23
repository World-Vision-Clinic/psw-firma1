using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        EventService service = new EventService(new EventRepository());

        [HttpGet]
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
            EventDto dto = new EventDto(ev.Id, ev.Name, ev.EventTime);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Save(Event newEvent)
        {
            service.Save(newEvent);
            return Ok();
        }

    }
}
