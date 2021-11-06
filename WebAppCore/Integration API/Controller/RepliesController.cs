using Integration.Pharmacy.Model;
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
        private PharmaciesService pharmaciesService = new PharmaciesService();
        private ObjectionsService objectionsService = new ObjectionsService();
        private RepliesService repliesService = new RepliesService();

        [HttpPost("add")]
        public IActionResult Add(ReplyDto dto)
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
            {
                return BadRequest("Api Key was not provided");
            }

            PharmacyProfile foundedPharmacy = new PharmacyProfile();
            foreach (PharmacyProfile pharmacy in pharmaciesService.GetAll())
            {
                if (extractedApiKey.Equals(pharmacy.Key))
                {
                    foundedPharmacy = pharmacy;
                    break;
                }
            }

            if (foundedPharmacy == null)
            {
                return BadRequest("Api Key is not valid!");
            }

            bool objectionExists = false;
            foreach(Objection objection in objectionsService.GetAll())
            {
                if (dto.ObjectionId.Equals(objection.Id))
                {
                    objectionExists = true;
                    break;
                }
            }

            if (!objectionExists)
            {
                return BadRequest("Objection doesn't exists");
            }

            if (dto.Content.Length <= 0)
            {
                return BadRequest();
            }

            repliesService.AddNewReply(ReplyMapper.ReplyDtoToReply(dto));

            System.Diagnostics.Debug.WriteLine(dto.Content);
            return Ok();
        }
    }
}
