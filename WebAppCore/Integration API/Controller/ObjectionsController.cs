using Integration.Pharmacy.Model;
using Integration_API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Pharmacy.Service;
using Integration_API.Mapper;
using Integration;
using RestSharp;
using Integration.Pharmacy.Repository;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ObjectionsController : ControllerBase
    {
        private ObjectionsService objectionsService = new ObjectionsService();
        private PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        private CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());

        [HttpGet]       
        public IActionResult Get()
        {
            List<Objection> objections = new List<Objection>();
            List<ObjectionDto> result = new List<ObjectionDto>();
            objections = objectionsService.GetAll();
            objections.ForEach(objection => result.Add(ObjectionMapper.ObjectionToObjectionDto(objection,pharmaciesService.Get(objection.PharmacyId).Name)));
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(ObjectionDto dto)
        {
            Credential credential = credentialsService.GetByPharmacyLocalhost(dto.PharmacyLocalhost);
            Objection newObjection = ObjectionMapper.ObjectionDtoToObjection(dto, Generator.GenerateObjectionId());

            var client = new RestSharp.RestClient(credential.PharmacyLocalhost);
            var request = new RestRequest("/objections/add");

            request.AddHeader("Content-Type", "application/json");
            
            request.AddJsonBody(
            new
            {
                Content = newObjection.Content,
                Id = newObjection.Id
            });
            request.AddHeader("ApiKey", credential.ApiKey);

            IRestResponse response = client.Post(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest();
            }

            objectionsService.saveEntity(newObjection);
            return Ok();
        }




    }
}
