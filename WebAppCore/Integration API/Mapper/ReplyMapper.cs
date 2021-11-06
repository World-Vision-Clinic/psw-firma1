using Integration.Pharmacy.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Mapper
{
    public class ReplyMapper
    {
        public static Reply ReplyDtoToReply(ReplyDto dto)
        {
            Reply reply = new Reply();
            reply.Content = dto.Content;
            reply.ObjectionId = dto.ObjectionId;
            return reply;
        }
    }
}
