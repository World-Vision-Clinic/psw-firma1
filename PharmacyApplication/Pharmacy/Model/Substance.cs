using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class Substance
    {
        public long SubstanceId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public long MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }

        public Substance() { }

        public Substance(long id, string name, double quantity, long medicineId)
        {
            SubstanceId = id;
            Name = name;
            Quantity = quantity;
            medicineId = medicineId;
        }

    }
}