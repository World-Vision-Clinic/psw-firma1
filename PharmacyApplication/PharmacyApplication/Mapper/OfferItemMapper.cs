using Pharmacy.Model;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Mapper
{
    public class OfferItemMapper
    {
        public static OfferItemDto OfferItemToOfferItemDto(OfferItem offerItem)
        {
            OfferItemDto dto = new OfferItemDto();
            dto.Dosage = offerItem.Dosage;
            dto.MedicineName = offerItem.MedicineName;
            dto.Price = offerItem.Price;
            dto.Quantity = offerItem.Quantity;
            return dto;
        }
    }
}
