using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.MedicalRecords.Service;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;
using Hospital_API.DTO;
using Hospital_API.Verification;
using Hospital.SharedModel;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        //private readonly HospitalContext _context;
        public PatientService _patientService { get; set; }
        public PatientAllergenService _patientAllergenService { get; set; }
        public AllergenService _allergenService { get; set; }
        public DoctorService _doctorService { get; set; }
        private PatientVerification _verification { get; set; }
        public PatientsController()
        {
            _patientService = new PatientService(new PatientRepository(new Hospital.SharedModel.HospitalContext()));
            _patientAllergenService = new PatientAllergenService(new PatientAllergenRepository(new Hospital.SharedModel.HospitalContext(), new PatientRepository(new Hospital.SharedModel.HospitalContext()), new AllergenRepository(new Hospital.SharedModel.HospitalContext())));
            _allergenService = new AllergenService(new AllergenRepository(new Hospital.SharedModel.HospitalContext()));
            _doctorService = new DoctorService(new DoctorRepository(new Hospital.SharedModel.HospitalContext()));
            _verification = new PatientVerification(_patientService, _doctorService, _allergenService);
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

        // POST: api/Patients/register
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("register")]
        public HttpResponseMessage RegisterPatient([FromBody] PatientRegisterDTO patientDTO)
        {
            if(!_verification.Verify(patientDTO))
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
            }
            Patient patient = patientDTO.ToPatient();
            _patientService.RegisterPatient(patient);
            foreach(int aid in patientDTO.Allergens)
            {
                patient = _patientService.FindByUserName(patientDTO.UserName);
                if(patient == null)
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
                _patientAllergenService.AddPatientAllergen(patient.Id, aid);
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK }; //TODO: Dodati smislene poruke ili redirect na "verifikacioni mejl poslat/resend"
        }

        // POST: api/Patients/test
        [HttpPost("test")]
        public HttpResponseMessage Test([FromBody] TestDTO testDTO)
        {
            Console.WriteLine(testDTO.Name);
            Console.WriteLine(testDTO.Count);
            Console.WriteLine("Test here");

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

    }
}
