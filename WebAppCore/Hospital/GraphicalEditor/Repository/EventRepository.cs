using Hospital.GraphicalEditor.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.GraphicalEditor.Repository
{
    public class EventRepository : IEventRepository
    {
        private EventsDbContextEditors dbContext = new EventsDbContextEditors();

        public List<Event> GetAll()
        {
            List<Event> events = new List<Event>();
            dbContext.EventsEditors.ToList().ForEach(newEvent => events.Add(newEvent));
            return events;
        }

        public void Save(Event newEvent)
        {
            dbContext.EventsEditors.Add(newEvent);
            dbContext.SaveChanges();
        }

        public Event GetById(int eventId)
        {
            return dbContext.EventsEditors.ToList().FirstOrDefault(e => e.Id == eventId);
        }

    }
}
