using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class TenderItem
    {
        public int TenderItemId { get; set; }
        public string MedicineName { get; set; }
        public double Dosage { get; set; }
        public int Quantity { get; set; }

    }
}
