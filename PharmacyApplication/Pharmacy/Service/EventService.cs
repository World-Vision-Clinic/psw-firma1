using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
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
