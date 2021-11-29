using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.Model;
using PharmacyApi.Protos;
using PharmacyAPI.Dto;

namespace PharmacyAPI.Mapper
{
    public class CredentialMapper
    {
        public static Credential CredentialDtoToCredential(CredentialDto dto)
        {
            Credential credential = new Credential();
            credential.HospitalName = dto.HospitalName;
            credential.HospitalLocalhost = dto.HospitalLocalhost;
            credential.ApiKey = dto.ApiKey;
            return credential;
        }

        public static Credential CredentialDtoToCredential(string HospitalName, string HospitalLocalhost, string ApiKey)
        {
            Credential credential = new Credential();
            credential.HospitalName = HospitalName;
            credential.HospitalLocalhost = HospitalLocalhost;
            credential.ApiKey = ApiKey;
            return credential;
        }
    }
}
