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
using Hospital_API.Mapper;
using Hospital.MedicalRecords.Service;
using Hospital.MedicalRecords.Repository;

namespace Hospital_API.Controllers
{
    [Route("api/onCallShifts")]
    [ApiController]
    public class OnCallShiftsController : ControllerBase
    {
        public OnCallShiftService _shiftsService { get; set; }
        public DoctorService _doctorService { get; set; }

        [ActivatorUtilitiesConstructor]
        public OnCallShiftsController()
        {
            HospitalContext context = new HospitalContext();
            OnCallShiftRepository shiftRepository = new OnCallShiftRepository(context);
            _shiftsService = new OnCallShiftService(shiftRepository);
            _doctorService = new DoctorService(new DoctorRepository(context));
        }

        public OnCallShiftsController(OnCallShiftService service)
        {
            _shiftsService = service;
        }

        [HttpGet("")]
        public ActionResult<List<OnCallShiftDTO>> getAll()
        {
            List<OnCallShiftDTO> list = new List<OnCallShiftDTO>();
            foreach(OnCallShift shift in _shiftsService.getAll().OrderBy(x => x.Id).ToList<OnCallShift>())
            {
                list.Add(OnCallShiftsMapper.onCallShiftToDTO(shift, _doctorService));
            }
            return list;
        }

        [HttpGet("doctorsDuty/{doctorId}")]
        public ActionResult<List<OnCallShiftDTO>> getDoctorsDuty(int doctorId)
        {
            List<OnCallShiftDTO> list = new List<OnCallShiftDTO>();
            foreach (OnCallShift shift in _shiftsService.getDoctorsDuty(doctorId).OrderBy(x => x.Id).ToList<OnCallShift>())
            {
                list.Add(OnCallShiftsMapper.onCallShiftToDTO(shift, _doctorService));
            }
            return list;
        }

        [HttpPost("")]
        public HttpResponseMessage addNew([FromBody] OnCallShiftDTO shift)
        {
            _shiftsService.addNewOnCallShift(shift.Id,shift.DoctorId,shift.Date);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpPut("")]
        public HttpResponseMessage updateShift([FromBody] OnCallShiftDTO shift)
        {
           // _shiftsService.deleteShift(shift.Id);
            _shiftsService.updateShift(shift.Id, shift.DoctorId, shift.Date);
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
