using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class ObjectionDto
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public ObjectionDto() { }

        public ObjectionDto(string content, string idEncoded)
        {
            Content = content;
            Id = idEncoded;
        }
    }
}
