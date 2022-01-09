using Pharmacy.Repository;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Model
{
    public class EventRepository: IEventRepository
    {
        private EventsDbContext dbContext = new EventsDbContext();

        public List<Event> GetAll()
        {
            List<Event> events = new List<Event>();
            dbContext.Events.ToList().ForEach(newEvent => events.Add(newEvent));
            return events;
        }

        public void Save(Event newEvent)
        {
            dbContext.Events.Add(newEvent);
            dbContext.SaveChanges();
        }

        public Event GetById(int eventId)
        {
            return dbContext.Events.ToList().FirstOrDefault(e => e.Id == eventId);
        }

    }
}
