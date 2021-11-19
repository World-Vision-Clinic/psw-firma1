using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class OrderedMedicineDTO
    {
        public string MedicineName { get; set; }
        public string Manufacturer { get; set; }
        public string SideEffects { get; set; }
        public string Usage { get; set; }
        public string Weigth { get; set; }
        public string MainPrecautions { get; set; }
        public string PotentialDangers { get; set; }
        public string Quantity { get; set; }
        public List<string> Replacements { get; set; }
        public double Price { get; set; }
    }
}
