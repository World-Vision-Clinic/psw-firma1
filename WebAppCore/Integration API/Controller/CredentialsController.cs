using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration_API.Mapper;
using Microsoft.AspNetCore.Mvc;

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
    }
}
