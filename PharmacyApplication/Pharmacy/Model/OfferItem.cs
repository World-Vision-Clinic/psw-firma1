using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class OfferItem
    {
        public int OfferItemId { get; set; }
        public string MedicineName { get; set; }
        public double Dosage { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public OfferItem(){}

        public OfferItem(int offerItemId, string medicineName, double dosage, int quantity, double price)
        {
            OfferItemId = offerItemId;
            MedicineName = medicineName;
            Dosage = dosage;
            Quantity = quantity;
            Price = price;
        }

        public OfferItem(string medicineName, double dosage, int quantity, double price)
        {
            MedicineName = medicineName;
            Dosage = dosage;
            Quantity = quantity;
            Price = price;
        }
    }
}
