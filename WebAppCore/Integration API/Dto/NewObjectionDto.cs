using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class NewObjectionDto
    {
        public string EncodedId { get; set; }
        public string Content { get; set; }

        public NewObjectionDto()
        {

        }

        public NewObjectionDto(string encodedId, string content)
        {
            EncodedId = encodedId;
            Content = content;
        }

    }
}
