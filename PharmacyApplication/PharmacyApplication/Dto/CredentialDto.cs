using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class CredentialDto
    {
        public string HospitalName { get; set; }
        public string HospitalLocalhost { get; set; }
        public string ApiKey { get; set; }

        public CredentialDto() { }

        public CredentialDto(string hospitalName, string hospitalLocalhost, string apiKey)
        {
            HospitalName = hospitalName;
            HospitalLocalhost = hospitalLocalhost;
            ApiKey = apiKey;
        }
    }
}
