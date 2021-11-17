using Pharmacy.Model;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Mapper
{
    public class ReplyMapper
    {
        public static Reply ReplyDtoToReply(ReplyDto dto)
        {
            Reply reply = new Reply();
            reply.ObjectionId = dto.ObjectionId;
            reply.Content = dto.Content;
            return reply;
        }
    }
}
