using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
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
            Event e = new Event(ev.Id, ev.Name, ev.EventTime);
            return Ok(e);
        }

        [HttpPost]
        public IActionResult Save(Event newEvent)
        {
            service.Save(newEvent);
            return Ok();
        }
    }
}
