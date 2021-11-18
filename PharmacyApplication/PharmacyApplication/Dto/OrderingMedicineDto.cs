using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class OrderingMedicineDto
    {
        public string MedicineName { get; set; }
        public string MedicineGrams { get; set; }
        public string NumOfBoxes { get; set; }
    }
}
