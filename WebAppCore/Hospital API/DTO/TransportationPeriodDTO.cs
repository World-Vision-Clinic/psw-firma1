using Hospital.GraphicalEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
