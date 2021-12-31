using Pharmacy.Model;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Mapper
{
    public class TenderOfferMapper
    {
        public static TenderOfferDto TenderOfferToTenderOfferDto(TenderOffer offer, string tenderId)
        {
            TenderOfferDto dto = new TenderOfferDto();
            dto.TenderHash = tenderId;
            dto.TenderOfferHash = offer.TenderOfferHash;
            dto.TotalPrice = offer.TotalPrice;
            dto.Winner = offer.Winner;
            List<OfferItemDto> offerItemDtos = new List<OfferItemDto>();
            foreach(OfferItem item in offer.OfferItems)
            {
                offerItemDtos.Add(OfferItemMapper.OfferItemToOfferItemDto(item));
            }
            dto.OfferItems = offerItemDtos;
            return dto;
        }
    }
}
