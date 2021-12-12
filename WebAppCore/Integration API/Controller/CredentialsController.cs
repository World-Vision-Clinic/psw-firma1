using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration_API.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        CredentialsService service = new CredentialsService(new CredentialsRepository());

        [HttpPost]
        public IActionResult Add(CredentialDto dto)
        {
            Credential newCredential = CredentialMapper.CredentialDtoToCredential(dto);
            if (!service.AddNewCredential(newCredential))
            {
                return BadRequest("Already exists!");
            }

            return Ok();
        } 

        [HttpGet("test")]
        public IActionResult Test(string id = "")
        {
            if (service.GetByPharmacyLocalhost(id) == null)
            {
                return BadRequest("Doesn't work :(");
            } 

            return Ok("Works!");
        }
    }
}
