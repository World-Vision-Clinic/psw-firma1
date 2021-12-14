using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class RenovationRequestDTO
    {
        public int Room1Id { get; set; }
        public int Room2Id { get; set; }
        public long StartPeriodTimestamp { get; set; }
        public long EndPeriodTimestamp { get; set; }
        public int DurationInDays { get; set; }
    }
}
