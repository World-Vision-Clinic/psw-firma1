using Integration_API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PharmaciesController : ControllerBase
    {
        [HttpPost("registerPharmacy")]
        public IActionResult Add(PharmacyDto dto)
        {
            if (dto.Name.Length <= 0 || dto.Localhost.Length <= 0)
            {
                return BadRequest();
            }

            //long id = Program.Products.Count > 0 ? Program.Products.Max(product => product.Id) + 1 : 1;
            //Product product = ProductAdapter.ProductDtoToProduct(dto);
            //product.Id = id;
            //Program.Products.Add(product);
            Console.WriteLine(dto.Name);
            return Ok();
        }

    }
}
