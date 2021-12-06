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
using Integration.Pharmacy.Repository;
using IntegrationAPI.Protos;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PharmaciesController : ControllerBase
    {

        PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        public const string HOSPITAL_NAME = "World Vision Clinic";
        public const string HOSPITAL_URL = "http://localhost:43818";
        public const string HOSPITAL_PORT = "127.0.0.1:3000";
        private readonly IWebHostEnvironment _env;


        public PharmaciesController(IWebHostEnvironment env)
        {
            _env = env;
        }
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

            if (dto.Protocol.Equals(ProtocolType.HTTP))
            {
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
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return BadRequest();
                return Ok();
            }
            else
            {
                var input = new RegisterPharmacyRequest { HospitalName = HOSPITAL_NAME, HospitalLocalhost = HOSPITAL_PORT, ApiKey = generatedKey};
                var channel = new Channel(dto.Localhost, ChannelCredentials.Insecure);
                var client = new gRPCService.gRPCServiceClient(channel);
                var reply = client.registerPharmacyAsync(input);
                if (reply.ResponseAsync.Result.Response.Equals("OK"))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
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

        [HttpPut]
        public IActionResult EditPharmacy(PharmacyDto dto)
        {
            if (dto.Name.Length <= 0 || dto.Localhost.Length <= 0 || dto.Address.Length <= 0 || dto.City.Length <= 0)
            {
                return BadRequest();
            }

            List<PharmacyProfile> pharmacies = pharmaciesService.GetAll();

            PharmacyProfile pharmacy = pharmaciesService.Edit(PharmacyMapper.PharmacyDtoToPharmacy(dto));
            if (pharmacy == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pharmacy);
            }
        }


        [HttpPost("addPhoto/{pharmacyName?}")]
        public JsonResult SavePhoto(String pharmacyName)
        {

            try { 
            var httpRequest = Request.Form;
            var postedFile = httpRequest.Files[0];
            //string filename = postedFile.FileName;
            var physicalPath = _env.ContentRootPath +"/../Integration"+ "/Photos/" + pharmacyName+".png";

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            return new JsonResult(pharmacyName + ".png");
        }
            catch
            {
                return new JsonResult("anonymous.png");
            }


        }



        [HttpDelete("deletePhoto/{pharmacyName?}")]       
        public IActionResult Delete(String pharmacyName)
        {
            var physicalPath = _env.ContentRootPath + "/../Integration" + "/Photos/" + pharmacyName + ".png";

            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
              
            }

            return Ok();
        }


    }
}
