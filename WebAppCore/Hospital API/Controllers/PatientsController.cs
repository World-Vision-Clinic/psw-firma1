using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Hospital.Service;
using Hospital.Repository;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        //private readonly HospitalContext _context;
        private readonly PatientService _patientService;

        public PatientsController()
        {
            _patientService = new PatientService(new PatientRepository(new Hospital.Models.HospitalContext()));
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var patient = _patientService.FindById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // GET: api/Patients/activate?token=
        [HttpGet("activate")]
        public IActionResult ActivatePatient([FromQuery]string token)
        {
            var patient = _patientService.FindByToken(token);

            if (patient == null)
            {
                return NotFound();
            }

            _patientService.Activate(patient);

            return Redirect("http://localhost:4200/login");
        }

    }
}
