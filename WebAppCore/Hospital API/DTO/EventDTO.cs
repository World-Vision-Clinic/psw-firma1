using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hspital_API.Dto
{
    public class EventDTO
    {
        public string Name { get; set; }

        public EventDTO(string name)
        {
            Name = name;
        }

        public EventDTO() { }
    }
}
