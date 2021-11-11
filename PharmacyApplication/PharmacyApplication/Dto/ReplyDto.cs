using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class ReplyDto
    { 
        public string ObjectionId { get; set; }
        public string Content { get; set; }

        public ReplyDto() { }

        public ReplyDto(string objectionId, string content)
        {
            ObjectionId = objectionId;
            Content = content;
        }
    }
}
