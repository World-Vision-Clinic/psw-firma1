using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class CredentialDto
    {
        public string PharmacyName { get; set; }
        public string PharmacyLocalhost { get; set; }
        public string ApiKey { get; set; }

        public CredentialDto() { }

        public CredentialDto(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public CredentialDto(string pharmacyName, string pharmacyLocalhost, string apiKey)
        {
            PharmacyName = pharmacyName;
            PharmacyLocalhost = pharmacyLocalhost;
            ApiKey = apiKey;
        }
    }
}
