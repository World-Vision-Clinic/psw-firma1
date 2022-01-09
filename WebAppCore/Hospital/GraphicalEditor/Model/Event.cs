using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventTime { get; set; }


        public Event()
        {
        }

        public Event(int id, string name, DateTime eventTime)
        {
            Id = id;
            Name = name;
            EventTime = eventTime;
        }
    }
}
