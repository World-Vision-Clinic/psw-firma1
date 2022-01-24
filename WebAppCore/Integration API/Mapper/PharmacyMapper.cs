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
            pharmacy.Address = new Address(dto.Address, dto.City);
            pharmacy.ConnectionInfo = new ConnectionInfo("key", dto.Localhost, dto.Protocol);
            pharmacy.Note = dto.Note;
            pharmacy.Email = dto.Email;
            return pharmacy;
        }

        public static PharmacyDto PharmacyToPharmacyDto(PharmacyProfile pharmacy)
        {
            PharmacyDto dto = new PharmacyDto();
            dto.Name = pharmacy.Name;
            dto.Localhost = pharmacy.ConnectionInfo.Domain;
            dto.Address = pharmacy.Address.Street;
            dto.City = pharmacy.Address.City;
            dto.Note = pharmacy.Note;
            dto.Protocol = pharmacy.ConnectionInfo.Protocol;
            dto.Email = pharmacy.Email;
            return dto;
        }
    }
}
