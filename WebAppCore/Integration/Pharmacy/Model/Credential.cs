using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    public class Credential
    {
        public int Id { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyLocalhost { get; set; }
        public string ApiKey { get; set; }

        public Credential() { }

        public Credential(int id, string pharmacyName, string pharmacyLocalhost, string apiKey)
        {
            Id = id;
            PharmacyName = pharmacyName;
            PharmacyLocalhost = pharmacyLocalhost;
            ApiKey = apiKey;
        }
    }
}
