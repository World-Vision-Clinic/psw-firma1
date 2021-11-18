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
using Integration.Pharmacy;
using Integration.Pharmacy.Model;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PharmaciesController : ControllerBase
    {

        PharmaciesService pharmaciesService = new PharmaciesService();
        public const string HOSPITAL_NAME = "World Vision Clinic";
        public const string HOSPITAL_URL = "http://localhost:43818";

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
            var request = new RestRequest("/credentials");

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
            new
            {
                   HospitalName = HOSPITAL_NAME,
                   HospitalLocalhost = HOSPITAL_URL,
                   ApiKey = generatedKey
            });

            IRestResponse response = client.Post(request);  // POST /credential  {"Name": "World Vision Clinic", "HospitalLocalhost": "http://localhost:43818", "ApiKey": "wqhegyqwegqyw21543"}
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<PharmacyProfile> pharmacies = new List<PharmacyProfile>();
            List<PharmacyDto> result = new List<PharmacyDto>();
            pharmacies = pharmaciesService.GetAll();
            pharmacies.ForEach(pharmacy => result.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy)));
            return Ok(result);
        }

        [HttpGet("Filtered")]
        public IActionResult Get(string searchFilter = "")
        {
            if (searchFilter != null)
            {
                List<PharmacyProfile> pharmacies = new List<PharmacyProfile>();
                List<PharmacyDto> result = new List<PharmacyDto>();
                pharmacies = pharmaciesService.GetFiltered(searchFilter);
                pharmacies.ForEach(pharmacy => result.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy)));
                return Ok(result);
            }
            else
            {
                List<PharmacyProfile> pharmacies = new List<PharmacyProfile>();
                List<PharmacyDto> result = new List<PharmacyDto>();
                pharmacies = pharmaciesService.GetAll();
                pharmacies.ForEach(pharmacy => result.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy)));
                return Ok(result);
            }
        }

    }
}
