using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using PharmacyAPI.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ObjectionsController : ControllerBase
    {
        ObjectionService service = new ObjectionService(new ObjectionsRepository());
        HospitalsService hospitalService = new HospitalsService(new HospitalsRepository());

        [HttpPost("add")]
        public IActionResult Add(ObjectionDto dto)
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
            {
                return BadRequest("Api Key was not provided");
            }

            Hospital hospital = hospitalService.GetHospitalByApiKey(extractedApiKey);
            if (hospital == null)
            {
                return BadRequest("Api Key is not valid!");
            }

            if (dto.Content.Length <= 0)
            {
                return BadRequest();
            }

            Objection newObjection = ObjectionMapper.ObjectionDtoToObjection(dto);
            newObjection.HospitalId = hospital.Localhost;
            if (!service.AddNewObjection(newObjection))
            {
                return BadRequest("Already exists!");
            }

            return Ok();
        }
    }
}
