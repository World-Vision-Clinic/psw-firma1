using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("[controller]/medicines")]
    [ApiController]
    public class TestController : ControllerBase
    {

        HospitalsService hospitalService = new HospitalsService(new HospitalsRepository());

        [HttpPost("OrderMedicine")]
        public IActionResult OrderMedicine(OrderingMedicineDto dto)
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

            if (dto.MedicineName.Equals("Brufen"))
            {
                return Ok();
            }
            return BadRequest();

        }
    }
}
