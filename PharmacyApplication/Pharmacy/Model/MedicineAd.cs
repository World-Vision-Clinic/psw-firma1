using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class MedicineAd
    {
        public long Id { get; set; }
        public long MedicineId { get; set; }
        public double PromotionPrice { get; set; }
        public MedicineAd() { }
    }
}
