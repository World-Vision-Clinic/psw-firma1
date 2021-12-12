using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Dto
{
    public class MedicineConsumptionDto
    {
        public DateTime Beginning { get; set; }
        public DateTime End { get; set; }

        public MedicineConsumptionDto() { }

        public MedicineConsumptionDto(DateTime beginning, DateTime end)
        {
            Beginning = beginning;
            End = end;
        }
    }
}
