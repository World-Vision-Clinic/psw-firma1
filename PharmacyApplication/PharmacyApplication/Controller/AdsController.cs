using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class AdsController: ControllerBase
    {
        AdsService service = new AdsService(new AdsRepository());

        [HttpGet]
        public IActionResult GetAll()
        {
            ICollection<Ad> ads = service.GetAll();
            return Ok(ads);
        }
    }
}
