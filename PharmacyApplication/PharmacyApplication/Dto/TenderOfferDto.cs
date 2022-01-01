using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class TenderOfferDto
    {
        public string TenderHash { get; set; }
        public string TenderOfferHash { get; set; }
        public double TotalPrice { get; set; }
        public virtual ICollection<OfferItemDto> OfferItems { get; set; }
        public bool Winner { get; set; }

        public TenderOfferDto()
        {
        }

        public TenderOfferDto(string tenderId, string tenderOfferHash, double totalPrice, ICollection<OfferItemDto> offerItems, bool winner)
        {
            TenderHash = tenderId;
            TenderOfferHash = tenderOfferHash;
            TotalPrice = totalPrice;
            OfferItems = offerItems;
            Winner = winner;
        }

        
    }
}
