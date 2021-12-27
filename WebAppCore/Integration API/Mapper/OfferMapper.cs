using Integration.Partnership.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Mapper
{
    public class OfferMapper
    {
        public static WinningOfferDto OfferToWinningOfferDto(TenderOffer offer, string tenderName)
        {
            WinningOfferDto dto = new WinningOfferDto();
            dto.TenderName = tenderName;
            dto.TotalPrice = offer.TotalPrice;
            return dto;
        }
    }
}
