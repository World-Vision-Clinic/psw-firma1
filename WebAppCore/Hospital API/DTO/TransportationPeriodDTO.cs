using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.GraphicalEditor.Model;

namespace Hospital_API.DTO
{
    public class TransportationPeriodDTO
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public TransportationPeriodDTO(DatePeriod datePeriod)
        {
            startDate = datePeriod.startDate;
            endDate = datePeriod.endDate;
        }

    }
}
