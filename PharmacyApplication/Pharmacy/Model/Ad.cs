using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class Ad
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? PromotionEndTime { get; set; }
        public virtual ICollection<MedicineAd> MedicinesOnPromotion { get; set; }

        public Ad() { }
    }
}
