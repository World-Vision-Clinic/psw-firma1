using Integration.Pharmacy.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Mapper
{
    public class PharmacyMapper
    {
        public static PharmacyProfile PharmacyDtoToPharmacy(PharmacyDto dto)
        {
            PharmacyProfile pharmacy = new PharmacyProfile();
            pharmacy.Name = dto.Name;
            pharmacy.Localhost = dto.Localhost;
            pharmacy.Protocol = ProtocolType.HTTP;
            pharmacy.Address = dto.Address;
            pharmacy.City = dto.City;
            pharmacy.Protocol = dto.Protocol;
            pharmacy.Note = dto.Note;
            return pharmacy;
        }

        public static PharmacyDto PharmacyToPharmacyDto(PharmacyProfile pharmacy)
        {
            PharmacyDto dto = new PharmacyDto();
            dto.Name = pharmacy.Name;
            dto.Localhost = pharmacy.Localhost;
            dto.Address = pharmacy.Address;
            dto.City = pharmacy.City;
            dto.Note = pharmacy.Note;
            dto.Protocol = pharmacy.Protocol;
            return dto;
        }
    }
}
