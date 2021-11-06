using Integration.Pharmacy.Model;
using Integration_API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Pharmacy.Service;
using Integration_API.Mapper;
using Integration;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ObjectionsController : ControllerBase
    {
        private ObjectionsService objectionsService = new ObjectionsService();
        private PharmaciesService pharmaciesService = new PharmaciesService();

        [HttpGet]       
        public IActionResult Get()
        {
            List<Objection> objections = new List<Objection>();
            List<ObjectionDto> result = new List<ObjectionDto>();
            objections = objectionsService.GetAll();
            objections.ForEach(objection => result.Add(ObjectionMapper.ObjectionToObjectionDto(objection,pharmaciesService.Get(objection.PharmacyId).Name)));
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(ObjectionDto dto)
        {
            Objection newObjection = ObjectionMapper.ObjectionDtoToObjection(dto, Generator.GenerateObjectionId());
            if(!objectionsService.sendObjection(newObjection))
            {
                return BadRequest("Error[Sending objection to pharmacy]");
            }


            objectionsService.saveEntity(newObjection);
            return Ok();
        }




    }
}
