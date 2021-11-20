using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class MedicineDto
    {
        public string Name { get; set; }
        public double DosageInMg { get; set; }
        public int Quantity { get; set; }

        public MedicineDto() { }

        public MedicineDto(string name, double dosageInMg, int quantity)
        {
            Name = name;
            DosageInMg = dosageInMg;
            Quantity = quantity;
        }
    }
}
