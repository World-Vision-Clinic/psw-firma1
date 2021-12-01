using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class AppointmentRecommendationRequestDTO
    {
        public DateTime LowerDateRange { get; set; }
        public DateTime UpperDateRange { get; set; }
        public string LowerTimeRange { get; set; }
        public string UpperTimeRange { get; set; }

        public int DoctorId { get; set; }
    }
}
