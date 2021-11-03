using Integration_API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Pharmacy.Service;
using Integration_API.Mapper;
using RestSharp;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PharmaciesController : ControllerBase
    {

        PharmaciesService pharmaciesService = new PharmaciesService();

        [HttpPost("registerPharmacy")]
        public IActionResult Add(PharmacyDto dto)
        {
            if (dto.Name.Length <= 0 || dto.Localhost.Length <= 0)
            {
                return BadRequest();
            }

            string generatedKey;
            if (!pharmaciesService.AddNewPharmacy(PharmacyMapper.PharmacyDtoToPharmacy(dto), out generatedKey))
            {
                return BadRequest();
            }

            var client = new RestSharp.RestClient(dto.Localhost);
            var request = new RestRequest("/credential");

            CredentialDto credential = new CredentialDto("World Vision Clinic", "http://localhost:43818", generatedKey);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
            new
            {
                   hospitalName = credential.HospitalName,
                   hospitalLocalhost = credential.HospitalLocalhost,
                   apiKey = credential.ApiKey
            });

            IRestResponse response = client.Post(request);  // POST /credential  {"Name": "World Vision Clinic", "HospitalLocalhost": "http://localhost:43818", "ApiKey": "wqhegyqwegqyw21543"}
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            return Ok(response);
        }

    }
}
