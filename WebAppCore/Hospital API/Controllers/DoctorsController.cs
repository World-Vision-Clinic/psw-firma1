using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.SharedModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalContext _context;
        private readonly DoctorService _doctorService;

        [ActivatorUtilitiesConstructor]
        public DoctorsController()
        {
            _context = new HospitalContext();
            PatientRepository _patientRepository = new PatientRepository(_context);
            _doctorService = new DoctorService(new DoctorRepository(_context, _patientRepository));
        }

        public DoctorsController(HospitalContext _context, DoctorService _doctorService)
        {
            this._context = _context;
            this._doctorService = _doctorService;
        }

        // GET: api/Doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return _doctorService.GetAll();
        }

        // GET: api/Doctors/available
        [HttpGet("available")]
        public ActionResult<IEnumerable<Doctor>> GetAvailableDoctors()
        {
            return _doctorService.GetAvailableDoctors();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public ActionResult<Doctor> GetDoctor(int id)
        {
            var doctor = _doctorService.FindById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }
        [HttpGet("GeForSpecialty/{specialty}")]
        public ActionResult<IEnumerable<Doctor>> GetDoctorForSpecialty(int specialty)
        {

            List<Doctor> doctors = _doctorService.GetForSpecialty(specialty);

            if (doctors.Count == 0)
            {
                return NotFound();
            }

            return Ok(doctors);
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }


    }
}
