using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Repository.RepositoryInterfaces;
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
    public class CredentialsController : ControllerBase
    {
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());
        [HttpPost]
        public IActionResult Add(CredentialDto dto)
        {
            Credential newCredential = CredentialMapper.CredentialDtoToCredential(dto);
            if (!credentialsService.AddNewCredential(newCredential))
            {
                return BadRequest("Already exists!");
            }

            return Ok();
        }
    }
}
