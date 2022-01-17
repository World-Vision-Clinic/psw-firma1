using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
    public class EventService
    {
        IEventRepository eventRepository;

        public List<Event> GetAll()
        {
            return eventRepository.GetAll();
        }

        public Event GetById(int eventId)
        {
            return eventRepository.GetById(eventId);
        }

        public void Save(Event newEvent)
        {
            eventRepository.Save(newEvent);
        }

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }
    }
}
