using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class OfferItemDto
    {
        public string MedicineName { get; set; }
        public double Dosage { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public OfferItemDto()
        {
        }

        public OfferItemDto(string medicineName, double dosage, int quantity, double price)
        {
            MedicineName = medicineName;
            Dosage = dosage;
            Quantity = quantity;
            Price = price;
        }
    }
}
