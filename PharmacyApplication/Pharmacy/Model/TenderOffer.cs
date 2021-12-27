using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class TenderOffer
    {
        public int TenderOfferId { get; set; }
        public string TenderOfferHash { get; set; }
        public double TotalPrice { get; set; }
        public virtual ICollection<OfferItem> OfferItems { get; set; }
        public bool Winner { get; set; }
    }
}
