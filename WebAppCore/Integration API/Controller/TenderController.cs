using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Integration_API.Dto;
using Integration_API.Mapper;
using Integration.Partnership.Service;
using Integration.Partnership.Repository;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
       
        TenderService service = new TenderService(new TenderRepository());
        [HttpPost]
        public IActionResult CreateTender(TenderCreationDto dto)
        {

            return Ok();
        }
    }
}