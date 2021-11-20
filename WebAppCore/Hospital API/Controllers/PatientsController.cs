using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.MedicalRecords.Service;
using Hospital.MedicalRecords.Repository;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        //private readonly HospitalContext _context;
        public PatientService _patientService { get; set; }

        public PatientsController()
        {
            _patientService = new PatientService(new PatientRepository(new Hospital.SharedModel.HospitalContext()));
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
