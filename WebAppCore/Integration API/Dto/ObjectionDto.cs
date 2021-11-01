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

        public ObjectionDto(string id, string content)
        {
            Id = id;
            Content = content;
        }

        public ObjectionDto() { }
    }
}
