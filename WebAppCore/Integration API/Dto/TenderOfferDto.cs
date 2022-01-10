using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class TenderOfferDto
    {
        public int TenderOfferId { get; set; }
        public string TenderOfferHash { get; set; }
        public string PharmacyName { get; set; }
        public double TotalPrice { get; set; }
        public bool Winner { get; set; }
        public string TenderHash { get; set; }

        public TenderOfferDto()
        { }
        public TenderOfferDto(int tenderOfferId, string tenderOfferHash, string pharmacyName, double totalPrice, bool winner)
        {
            TenderOfferId = tenderOfferId;
            TenderOfferHash = tenderOfferHash;
            PharmacyName = pharmacyName;
            TotalPrice = totalPrice;
            Winner = winner;
        }
    }
}
