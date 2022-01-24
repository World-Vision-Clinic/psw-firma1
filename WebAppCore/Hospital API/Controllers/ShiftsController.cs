using Hospital.SharedModel;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Service;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.ShiftsAndVacations.Model;
using System.Net.Http;
using System.Net;
using Hospital_API.DTO;

namespace Hospital_API.Controllers
{
    [Route("api/shifts")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        public ShiftService _shiftsService { get; set; }

        [ActivatorUtilitiesConstructor]
        public ShiftsController()
        {
            HospitalContext context = new HospitalContext();
            IShiftRepository shiftRepository = new ShiftRepository(context);
            _shiftsService = new ShiftService(shiftRepository);
        }

        public ShiftsController(ShiftService service)
        {
            _shiftsService = service;
        }

        [HttpGet("getAll")]
        public ActionResult<List<Shift>> getAll()
        {
            return _shiftsService.getAll();
        }

        [HttpPost("newShift")]
        public HttpResponseMessage addNew([FromBody] ShiftDTO shift)
        {
            _shiftsService.addNewShift(shift.Id,shift.Name,shift.Start,shift.End);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpPost("update")]
        public HttpResponseMessage updateShift([FromBody] ShiftDTO shift)
        {
            _shiftsService.deleteShift(shift.Id);
            _shiftsService.addNewShift(shift.Id, shift.Name, shift.Start, shift.End);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpDelete("{id}")]
        public ActionResult<Shift> deleteShift(int id)
        {
            if (_shiftsService.deleteShift(id))
            {
                return Ok();
            }
            else
            {
                return NotFound(); 
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<Shift> GetOneShift(int id)
        {
            var shift = _shiftsService.findById(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }
    }
}
