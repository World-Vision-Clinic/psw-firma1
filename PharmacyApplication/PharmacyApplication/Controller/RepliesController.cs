using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using PharmacyAPI.Mapper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        RepliesService service = new RepliesService(new RepliesRepository());
        ObjectionService objectionsService = new ObjectionService(new ObjectionsRepository());
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());

        [HttpPost("add")]
        public IActionResult Add(ReplyDto dto)
        {
            if(dto.Content.Length <= 0 || dto.ObjectionId.Length <= 0)
            {
                return BadRequest();
            }

            Objection objection = objectionsService.GetObjectionById(dto.ObjectionId);
            Credential credential = credentialsService.GetByHospitalLocalhost(objection.HospitalId);

            var client = new RestSharp.RestClient(credential.HospitalLocalhost);
            var request = new RestRequest("/replies/add");
            request.AddHeader("ApiKey", credential.ApiKey);

            request.AddJsonBody(dto);
            IRestResponse response = client.Post(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(response.Content);
            }

            Reply newReply = ReplyMapper.ReplyDtoToReply(dto);
            service.AddNewReply(newReply);
            return Ok();
        }
    }
}
