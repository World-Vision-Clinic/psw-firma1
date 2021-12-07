using Integration.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class PharmacyDto
    {
        public string Name { get; set; }
        public string Localhost { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ProtocolType Protocol { get; set; }
        public string Note { get; set; }
    }
}
