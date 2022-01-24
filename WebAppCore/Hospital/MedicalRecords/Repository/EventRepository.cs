using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class EventRepository : IEventRepository
    {
        private EventsDbContext dbContext = new EventsDbContext();

        public List<Event> GetAll()
        {
            List<Event> events = new List<Event>();
            dbContext.EventsHospital.ToList().ForEach(newEvent => events.Add(newEvent));
            return events;
        }



        public void Save(Event newEvent)
        {
            dbContext.EventsHospital.Add(newEvent);
            dbContext.SaveChanges();
        }

        public Event GetById(int eventId)
        {
            return dbContext.EventsHospital.ToList().FirstOrDefault(e => e.Id == eventId);
        }

        public Event getLastEvent()
        {
            return dbContext.EventsHospital.OrderByDescending(p => p.Id).FirstOrDefault();
        }

    }
}
