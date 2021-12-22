using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Partnership.Model
{
    public class TenderOffer
    {
        public int TenderOfferId { get; set; }
        public string TenderOfferHash { get; set; }
        public string PharmacyName { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<OfferItem> OfferItems { get; set; }
    }
}
