using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Ionic.Zip;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = Integration.Pharmacy.Model.File;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        FilesRepository repository = new FilesRepository();

        [HttpGet]
        public IActionResult GetAllFiles()
        {
            return Ok(repository.GetAll().Where(n => n.Extension == "pdf").Select(n => new Integration.Pharmacy.Model.File
            {
                Id = n.Id,
                Name = n.Name,
                Extension = n.Extension,
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = repository.GetAll().Where(n => n.Id == id).FirstOrDefault();

            var memory = new MemoryStream();
            using (var stream = new FileStream(file.Path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = Path.GetFileName(file.Path);

            return File(memory, contentType, fileName);
        }
    }
}
