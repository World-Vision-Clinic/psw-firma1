using Integration.Pharmacy.Model;
using Integration_API.Dto;
using Integration_API.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add(ReplyDto dto)
        {
            Reply newReply = ReplyMapper.ReplyDtoToReply(dto);
            System.Diagnostics.Debug.WriteLine(dto.Content);
            return Ok();
        }
    }
}
