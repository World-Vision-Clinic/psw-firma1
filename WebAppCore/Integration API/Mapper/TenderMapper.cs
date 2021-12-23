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
            newTender.TenderItems = dto.TenderItems;
            newTender.EndTime = dto.EndTime;
            return newTender;
        }

        public static TenderDto TenderToTenderDto(Tender tender)
        {
            TenderDto dto = new TenderDto();
            dto.TenderHash = tender.TenderHash;
            dto.Title = tender.Title;
            dto.Description = tender.Description;
            dto.TenderItems = tender.TenderItems;
            dto.EndTime = tender.EndTime;
            return dto;
        }
    }
}
