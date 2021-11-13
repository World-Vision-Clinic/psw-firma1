using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class ObjectionDto
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public string PharmacyName { get; set; }
        public string PharmacyLocalhost { get; set; }

        public ObjectionDto(string content, string pharmacyName, string pharmacyLocalhost)
        {
            Id = " ";
            Content = content;
            PharmacyName = pharmacyName;
            PharmacyLocalhost = pharmacyLocalhost;
        }

        public ObjectionDto() { }
    }
}
