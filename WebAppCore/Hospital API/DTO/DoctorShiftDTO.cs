using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class DoctorShiftDTO
    {
        public int doctorId { get; set; }
        public int shiftId { get; set; }

        public DoctorShiftDTO() { }
    }
}
