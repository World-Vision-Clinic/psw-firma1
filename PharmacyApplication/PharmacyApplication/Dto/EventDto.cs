using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventTime { get; set; }
        public EventDto()
        {
        }

        public EventDto(int id, string name, DateTime eventTime)
        {
            Id = id;
            Name = name;
            EventTime = eventTime;
        }
    }
}
