using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class ReplyDto
    {
        public string Id { get; set; }
        public string ObjectionId { get; set; }
        public string Content { get; set; }

        public ReplyDto(string id, string objectionId, string content)
        {
            Id = id;
            ObjectionId = objectionId;
            Content = content;
        }

        public ReplyDto() { }
    }
}

