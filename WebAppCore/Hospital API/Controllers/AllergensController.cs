using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.SharedModel;
using Microsoft.AspNetCore.Mvc;
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
    public class AllergensController : ControllerBase
    {
        private readonly HospitalContext _context;
        private readonly AllergenService _allergenService;
        public bool test = false;

        public AllergensController()
        {
            _context = new HospitalContext();
            _allergenService = new AllergenService(new AllergenRepository(_context));
        }

        // GET: api/Allergens
        [HttpGet]
        public ActionResult<IEnumerable<Allergen>> GetAllergens()
        {
            return _allergenService.GetAllergens(); //TODO: Zameniti u GetAll, i to svugde
        }

        // GET: api/Allergens/5
        [HttpGet("{id}")]
        public ActionResult<Allergen> GetAllergen(int id)
        {
            var allergen = _allergenService.FindById(id);

            if (allergen == null)
            {
                return NotFound();
            }

            return allergen;
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}
