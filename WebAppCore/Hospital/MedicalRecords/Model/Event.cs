using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventTime { get; set; }
        public TimeSpan TimeDifference { get; set; }


        public Event()
        {
        }

        public Event(int id, string name, DateTime eventTime,TimeSpan timeDifference)
        {
            Id = id;
            Name = name;
            EventTime = eventTime;
            TimeDifference = timeDifference;
        }
    }
}
