using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Integration_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;

        public PhotosController(IWebHostEnvironment env)
        {
            _env = env;
        }


        [HttpPost("addPhoto/{pharmacyName?}")]
        public JsonResult SavePhoto(String pharmacyName)
        {

            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                //string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/../Integration" + "/Photos/" + pharmacyName + ".png";

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(pharmacyName + ".png");
            }
            catch
            {
                return new JsonResult("anonymous.png");
            }


        }



        [HttpDelete("deletePhoto/{pharmacyName?}")]
        public IActionResult Delete(String pharmacyName)
        {
            var physicalPath = _env.ContentRootPath + "/../Integration" + "/Photos/" + pharmacyName + ".png";

            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);

            }

            return Ok();
        }
    }
}
