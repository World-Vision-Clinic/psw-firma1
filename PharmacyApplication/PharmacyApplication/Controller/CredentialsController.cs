using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Repository.RepositoryInterfaces;
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
    public class CredentialsController : ControllerBase
    {
        CredentialsService service = new CredentialsService(new CredentialsRepository());
        HospitalsService hospitalsService = new HospitalsService(new HospitalsRepository());
        public const string PHARMACY_NAME = "Jankovic";
        public const string PHARMACY_URL = "http://localhost:8081";


        [HttpPost]
        public IActionResult Add(CredentialDto dto)
        {
            Credential newCredential = CredentialMapper.CredentialDtoToCredential(dto);
            if (!service.AddNewCredential(newCredential))
            {
                return BadRequest("Already exists!");
            }

            string generatedApiKey = Generator.GenerateApiKey();

            var client = new RestSharp.RestClient(dto.HospitalLocalhost);
            var request = new RestRequest("/credentials");

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
            new
            {
                PharmacyName = PHARMACY_NAME,
                PharmacyLocalhost = PHARMACY_URL,
                ApiKey = generatedApiKey
            });

            IRestResponse response = client.Post(request);
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
            Hospital newHospital = new Hospital { Localhost = dto.HospitalLocalhost, Name = dto.HospitalName, Key = generatedApiKey };
            newHospital.Key = generatedApiKey;
            if (!hospitalsService.AddNewHospital(newHospital))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
