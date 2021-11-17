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
    public class HospitalsController : ControllerBase
    {
        HospitalsService service = new HospitalsService(new HospitalsRepository());

        [HttpPost("registerHospital")]
        public IActionResult Add(HospitalDto dto)
        {
            if(dto.Name.Length <= 0 || dto.Localhost.Length <= 0)
            {
                return BadRequest();
            }

            string generatedApiKey = Generator.GenerateApiKey();

            var client = new RestSharp.RestClient(dto.Localhost);
            var request = new RestRequest("/credentials");

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
            new
            {
                PharmacyName = "Jankovic",
                PharmacyLocalhost = "http://localhost:34616",
                ApiKey = generatedApiKey
            });

            IRestResponse response = client.Post(request);
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
            Hospital newHospital = HospitalMapper.HospitalDtoToHospital(dto);
            newHospital.Key = generatedApiKey;
            if (!service.AddNewHospital(newHospital))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
