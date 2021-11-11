using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class Credential
    {
        public int Id { get; set; }
        public string HospitalName { get; set; }
        public string HospitalLocalhost { get; set; }
        public string ApiKey { get; set; }

        public Credential() { }

        public Credential(int id, string hospitalName, string hospitalLocalhost, string apiKey)
        {
            Id = id;
            HospitalName = hospitalName;
            HospitalLocalhost = hospitalLocalhost;
            ApiKey = apiKey;
        }
    }
}
