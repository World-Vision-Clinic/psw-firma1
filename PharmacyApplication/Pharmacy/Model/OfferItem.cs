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
    }
}
