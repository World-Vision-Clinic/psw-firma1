using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        PharmacyHTTPConnection httpConnection = new PharmacyHTTPConnection();
        PharmacyGRPConnection grpcConnection = new PharmacyGRPConnection();

        [HttpPost("registerPharmacy")]
        public IActionResult Add(PharmacyDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Localhost))
                return BadRequest("Please fill all fields");

            string generatedKey;
            if (!pharmaciesService.AddNewPharmacy(PharmacyMapper.PharmacyDtoToPharmacy(dto), out generatedKey))
                return BadRequest("Pharmacy already exists!");

            if (SendRegistrationRequest(dto, generatedKey)) return Ok();
            else return BadRequest("Unable to register to pharmacy!");
        }

        private bool SendRegistrationRequest(PharmacyDto dto, string key)
        {
            if (dto.Protocol.Equals(ProtocolType.HTTP))
                return httpConnection.SendRegistrationRequestHttp(dto, key);
            else
                return grpcConnection.SendRegistrationRequestGrpc(dto, key);
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
            List<PharmacyProfile> pharmacies = new List<PharmacyProfile>();
            List<PharmacyDto> result = new List<PharmacyDto>();
            if (searchFilter != null)
                pharmacies = pharmaciesService.GetFiltered(searchFilter);
            else
                pharmacies = pharmaciesService.GetAll();
            pharmacies.ForEach(pharmacy => result.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy)));
            return Ok(result);
        }

        [HttpPut]
        public IActionResult EditPharmacy(PharmacyDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Localhost) || string.IsNullOrEmpty(dto.Address) || string.IsNullOrEmpty(dto.City) || string.IsNullOrEmpty(dto.Email))
                return BadRequest("Please fill all fileds");
            PharmacyProfile pharmacy = pharmaciesService.Edit(PharmacyMapper.PharmacyDtoToPharmacy(dto));
            if (pharmacy == null) return NotFound("Pharmacy not found!");
            return Ok(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
        }

    }
}
