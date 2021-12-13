using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class PrescriptionDto
    {
        public string PatientName { get; set; }
        public int DurationInDays { get; set; }
        public int TimesPerDay { get; set; }
        public string MedicineName { get; set; }
        public string DosageInMg { get; set; }
        public string Quantity { get; set; }
    }
}
