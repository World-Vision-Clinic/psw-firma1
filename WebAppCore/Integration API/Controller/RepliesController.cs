using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
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
        private PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        private ObjectionsService objectionsService = new ObjectionsService();
        private RepliesService repliesService = new RepliesService();

        [HttpPost("add")]
        public IActionResult Add(ReplyDto dto)
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
                return BadRequest("Api Key was not provided");

            PharmacyProfile foundedPharmacy = pharmaciesService.GetAll().SingleOrDefault(p => p.ConnectionInfo.Key.Equals(extractedApiKey));

            if (foundedPharmacy == null)
                return BadRequest("Api Key is not valid!");

            Objection foundedObjection = objectionsService.GetAll().SingleOrDefault(o => o.Id.Equals(dto.ObjectionId));
           
            if (foundedObjection != null)
                return BadRequest("Objection doesn't exists");

            if (dto.Content.Length <= 0)
                return BadRequest("Invalid content!");

            repliesService.AddNewReply(ReplyMapper.ReplyDtoToReply(dto));
            return Ok();
        }

        [HttpGet("GetObjectionReplies")]
        public IActionResult GetObjectionReplies(string objectionId = "")
        {
            List<Reply> replies = new List<Reply>();
            List<String> result = null;
            replies = repliesService.GetObjectionReplies(objectionId.ToString());
            System.Diagnostics.Debug.WriteLine(objectionId);
            if(replies != null)
            {
                result = new List<String>();
                foreach(Reply repl in replies){ result.Add(ReplyMapper.ReplyToContent(repl));}
            }
            return Ok(result);
        }
    }
}
