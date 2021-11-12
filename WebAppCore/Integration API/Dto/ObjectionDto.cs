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

        public string PharmacyLocalhost { get; set; }

        public ObjectionDto(string id, string content, string pharmacyName)
        {
            Id = id;
            Content = content;
            PharmacyLocalhost = pharmacyName;
        }

        public ObjectionDto(string content, string pharmacyLocalhost)
        {
            Id = " ";
            Content = content;
            PharmacyLocalhost = pharmacyLocalhost;
        }

        public ObjectionDto() { }
    }
}
