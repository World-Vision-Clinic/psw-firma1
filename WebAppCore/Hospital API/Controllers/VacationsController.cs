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
using Hospital_API.Mapper;

namespace Hospital_API.Controllers
{
    [Route("api/vacations")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        public VacationService vacationService { get; set; }
        public DoctorService doctorService { get; set; }

        [ActivatorUtilitiesConstructor]
        public VacationsController()
        {
            HospitalContext context = new HospitalContext();
            IVacationRepository vacationRepository = new VacationRepository(context);
            IDoctorRepository doctorRepository = new DoctorRepository(context);
            vacationService = new VacationService(vacationRepository);
            doctorService = new DoctorService(doctorRepository);
        }

        public VacationsController(VacationService serviceV, DoctorService serviceD)
        {
            vacationService = serviceV;
            doctorService = serviceD;
        }

        [HttpGet]
        public ActionResult<List<VacationDTO>> getAll()
        {
            List<VacationDTO> list = new List<VacationDTO>();
            foreach (Vacation vacation in vacationService.getAll().OrderBy(x => x.Id).ToList<Vacation>())
            {
                list.Add(VacationsMapper.vacationToDTO(vacation, doctorService));
            }
            return list; 
        }

        [HttpPost]
        public HttpResponseMessage addNew([FromBody] VacationDTO v)
        {
            vacationService.addNewVacation(v.Id, v.Description, v.Start, v.End, v.DoctorId, v.FullName);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpPut]
        public HttpResponseMessage updateVacation([FromBody] VacationDTO v)
        {
            vacationService.updateVacation(v.Id, v.Description, v.Start, v.End,v.DoctorId, v.FullName);
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
