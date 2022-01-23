using Integration.Partnership.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Mapper
{
    public class TenderMapper
    {
        public static Tender TenderCreationDtoToTender(TenderCreationDto dto, String generatedId)
        {
            Tender newTender = new Tender();
            newTender.TenderHash = generatedId;
            newTender.Title = dto.Title;
            newTender.Description = dto.Description;
            foreach(TenderItem tenderItem in dto.TenderItems)
            {
                newTender.AddTenderItem(tenderItem.MedicineName, tenderItem.Dosage, tenderItem.Quantity);
            }
            newTender.EndTime = dto.EndTime;
            return newTender;
        }

        public static TenderDto TenderToTenderDto(Tender tender)
        {
            TenderDto dto = new TenderDto();
            dto.TenderHash = tender.TenderHash;
            dto.Title = tender.Title;
            dto.Description = tender.Description;
            dto.TenderItems = (ICollection<TenderItem>)tender.TenderItems;
            dto.TenderOffers = (ICollection<TenderOffer>)tender.TenderOffers;
            dto.EndTime = tender.EndTime;
            return dto;
        }

        public static Tender TenderDtoToTender(TenderDto dto)
        {
            Tender tender = new Tender();
            tender.Description = dto.Description;
            tender.EndTime = dto.EndTime;
            tender.TenderHash = dto.TenderHash;
            //tender.TenderItems = dto.TenderItems;
            tender.Title = dto.Title;
            //tender.TenderOffers = dto.TenderOffers;

            return tender;
        }

        public static TenderOffer TenderOfferDtoToTenderOffer(TenderOfferDto dto)
        {
            TenderOffer offer = new TenderOffer();
            offer.PharmacyName = dto.PharmacyName;
            offer.TenderOfferHash = dto.TenderOfferHash;
            offer.TenderOfferId = dto.TenderOfferId;
            offer.TotalPrice = dto.TotalPrice;
            offer.Winner = dto.Winner;
            offer.TenderHash = dto.TenderHash;

            return offer;
        }

        public static TenderDto TenderToTenderCloseDto(Tender tender)
        {
            TenderDto dto = new TenderDto();
            dto.TenderHash = tender.TenderHash;
            dto.EndTime = tender.EndTime;
            return dto;
        }
    }
}
