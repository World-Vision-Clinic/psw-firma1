using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class OrderingMedicineDTO
    {
        public string Localhost { get; set; }
        public string MedicineName { get; set; }
        public string MedicineGrams { get; set; }
        public string NumOfBoxes{ get; set; }

        public OrderingMedicineDTO(string localhost, string medicineName, string medicineGrams, string numOfBoxes)
        {
            Localhost = localhost;
            MedicineName = medicineName;
            MedicineGrams = medicineGrams;
            NumOfBoxes = numOfBoxes;
        }
    }
}
