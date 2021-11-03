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
            return pharmacy;
        }
    }
}
