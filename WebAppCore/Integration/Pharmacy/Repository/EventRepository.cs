using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    public class EventRepository : IEventRepository
    {
        private EventDbContext dbContext = new EventDbContext();

        public List<Event> GetAll()
        {
            List<Event> events = new List<Event>();
            dbContext.EventsIntegration.ToList().ForEach(newEvent => events.Add(newEvent));
            return events;
        }

        public void Save(Event newEvent)
        {
            dbContext.EventsIntegration.Add(newEvent);
            dbContext.SaveChanges();
        }

        public Event GetById(int eventId)
        {
            return dbContext.EventsIntegration.ToList().FirstOrDefault(e => e.Id == eventId);
        }

    }
}
