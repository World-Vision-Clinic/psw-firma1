using Integration.Pharmacy.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Mapper
{
    public class CredentialMapper
    {
        public static Credential CredentialDtoToCredential(CredentialDto dto)
        {
            Credential credential = new Credential();
            credential.PharmacyName = dto.PharmacyName;
            credential.PharmacyLocalhost = dto.PharmacyLocalhost;
            credential.ApiKey = dto.ApiKey;
            return credential;
        }
    }
}
