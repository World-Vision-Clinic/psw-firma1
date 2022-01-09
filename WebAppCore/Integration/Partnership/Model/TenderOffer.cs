using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Integration.Partnership.Model
{
    public class TenderOffer
    {
        public int TenderOfferId { get; set; }
        public string TenderOfferHash { get; set; }
        [NotMapped]
        public string TenderHash { get; set; }
        public string PharmacyName { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<OfferItem> OfferItems { get; set; }
        public bool Winner { get; set; }
    }
}
