using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
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

        public OrderedMedicineDTO()
        {
        }

        public OrderedMedicineDTO(string medicineName, string manufacturer, string sideEffects, string usage, string weigth, string mainPrecautions, string potentialDangers, string quantity, List<string> replacements, double price)
        {
            MedicineName = medicineName;
            Manufacturer = manufacturer;
            SideEffects = sideEffects;
            Usage = usage;
            Weigth = weigth;
            MainPrecautions = mainPrecautions;
            PotentialDangers = potentialDangers;
            Quantity = quantity;
            Replacements = replacements;
            Price = price;
        }
    }
}
