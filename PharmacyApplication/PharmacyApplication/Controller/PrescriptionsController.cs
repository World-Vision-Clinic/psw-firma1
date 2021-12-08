using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using Renci.SshNet;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        HospitalsService hospitalService = new HospitalsService(new HospitalsRepository());
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());

        [HttpPost("DownloadPrescription")]
        public IActionResult DownloadPrescription(NotificationPdfDownloadDto dto)
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
            {
                return BadRequest("Api Key was not provided");
            }

            Hospital hospital = hospitalService.GetHospitalByApiKey(extractedApiKey);
            if (hospital == null)
            {
                return BadRequest("Api Key is not valid!");
            }
            DownloadPdfFile(dto.FileName);

            return Ok("File is downloaded");
        }

        public void DownloadPdfFile(String filename)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", "user", "password")))
            {
                client.Connect();
                string serverFile = @"\public\" + filename;
                string localFile = filename;
                using (Stream stream = System.IO.File.OpenWrite(localFile))
                {
                    client.DownloadFile(serverFile, stream, x => { Console.WriteLine(x); });
                }
                client.Disconnect();
            }
        }
    }
}