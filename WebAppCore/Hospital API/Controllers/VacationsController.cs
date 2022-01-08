using Hospital.SharedModel;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Service;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using Hospital.MedicalRecords.Service;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;
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
    [Route("api/vacations")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        public VacationService vacationService { get; set; }
        //public DoctorService doctorService { get; set; }

        [ActivatorUtilitiesConstructor]
        public VacationsController()
        {
            HospitalContext context = new HospitalContext();
            IVacationRepository vacationRepository = new VacationRepository(context);
            IDoctorRepository doctorRepository = new DoctorRepository(context);
            vacationService = new VacationService(vacationRepository, doctorRepository);
            //doctorService = new DoctorService(doctorRepository);
        }

        public VacationsController(VacationService serviceV, DoctorService serviceD)
        {
            vacationService = serviceV;
            //doctorService = serviceD;
        }

        [HttpGet]
        public ActionResult<List<Vacation>> getAll()
        {
            return vacationService.getAll();
        }

        [HttpPost]
        public HttpResponseMessage addNew([FromBody] VacationDTO v)
        {
            //Doctor doctor = doctorService.FindById(v.DoctorId);
            vacationService.addNewVacation(v.Id, v.Description, v.Start, v.End, v.DoctorId);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpPost("update")]
        public HttpResponseMessage updateVacation([FromBody] VacationDTO v)
        {
            vacationService.deleteVacation(v.Id);
            //Doctor doctor = doctorService.FindById(v.DoctorId);
            vacationService.addNewVacation(v.Id, v.Description, v.Start, v.End,v.DoctorId);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpDelete("{id}")]
        public ActionResult<Vacation> deleteVacation(int id)
        {
            if (vacationService.deleteVacation(id))
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
