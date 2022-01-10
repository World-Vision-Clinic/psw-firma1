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
    public class OnCallShiftsController : ControllerBase
    {
        public OnCallShiftService _shiftsService { get; set; }

        [ActivatorUtilitiesConstructor]
        public OnCallShiftsController()
        {
            HospitalContext context = new HospitalContext();
            IOnCallShiftRepository shiftRepository = new OnCallShiftRepository(context);
            _shiftsService = new OnCallShiftService(shiftRepository);
        }

        public OnCallShiftsController(OnCallShiftService service)
        {
            _shiftsService = service;
        }

        [HttpGet("getAll")]
        public ActionResult<List<OnCallShift>> getAll()
        {
            return _shiftsService.getAll();
        }

        [HttpPost("newShift")]
        public HttpResponseMessage addNew([FromBody] OnCallShiftDTO shift)
        {
            _shiftsService.addNewOnCallShift(shift.Id,shift.Doctor.Id,shift.Date);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpPost("update")]
        public HttpResponseMessage updateShift([FromBody] OnCallShiftDTO shift)
        {
           // _shiftsService.deleteShift(shift.Id);
            _shiftsService.updateShift(shift.Id, shift.Doctor.Id, shift.Date);
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
    }
}
