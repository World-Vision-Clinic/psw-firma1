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
        public TimeSpan LowerTimeRange { get; set; }
        public TimeSpan UpperTimeRange { get; set; }
        public TimeSpan AppointmentLength { get; set; }

        public int DoctorId { get; set; }
    }
}
