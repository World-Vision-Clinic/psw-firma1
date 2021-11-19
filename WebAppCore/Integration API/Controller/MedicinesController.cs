using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration_API.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        private CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());

        [HttpGet("check")]
        public IActionResult CheckMedicineAvailability(string name = "", string dosage = "", string quantity = "")
        {
            if (name.Length <= 0 || dosage.Length <= 0 || quantity.Length <= 0)
            {
                return BadRequest();
            }

            MedicineDto medicineDto;
            try
            {
                medicineDto = new MedicineDto { Name = name, DosageInMg = Double.Parse(dosage), Quantity = Int32.Parse(quantity) };
            }
            catch
            {
                return BadRequest();
            }

            List<PharmacyDto> pharmaciesWithMedicine = new List<PharmacyDto>();

            foreach(PharmacyProfile pharmacy in pharmaciesService.GetAll())
            {
                if (SendRequestToCheckAvailability(pharmacy, medicineDto))
                {
                    pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
                }
            }

            return Ok(pharmaciesWithMedicine);
        }

        [HttpGet("spec")]
        public IActionResult GetSpecification(string pharmacyLocalhost = "", string medicine = "")
        {
            if (pharmacyLocalhost.Length <= 0 || medicine.Length <= 0)
            {
                return BadRequest();
            }

            if(!SendRequestForSpecification(pharmacyLocalhost, medicine))
            {
                return BadRequest();
            }

            if (!DownloadSpecification())
            {
                return BadRequest("Unable to download specification file");
            }

            return Ok();
        }

        private bool SendRequestForSpecification(string pharmacyLocalhost, string medicineName)
        {
            var client = new RestSharp.RestClient(pharmacyLocalhost);
            var request = new RestRequest("/medicines/spec?name=" + medicineName);

            Credential credential = credentialsService.GetByPharmacyLocalhost(pharmacyLocalhost);

            request.AddHeader("ApiKey", credential.ApiKey);

            IRestResponse response = client.Get(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        private bool DownloadSpecification()
        {
            try
            {
                using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.21", "user", "password")))
                {
                    client.Connect();

                    string serverFile = @"\public\Specification.txt";
                    string localFile = @"Specification.txt";
                    using (Stream stream = System.IO.File.OpenWrite(localFile))
                    {
                        client.DownloadFile(serverFile, stream);
                    }

                    client.Disconnect();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SendRequestToCheckAvailability(PharmacyProfile pharmacy, MedicineDto medicineDto)
        {
            var client = new RestSharp.RestClient(pharmacy.Localhost);
            var request = new RestRequest("/medicines/check?name=" + medicineDto.Name + "&dosage=" + medicineDto.DosageInMg + "&quantity=" + medicineDto.Quantity);

            Credential credential = credentialsService.GetByPharmacyLocalhost(pharmacy.Localhost);

            request.AddHeader("ApiKey", credential.ApiKey);

            IRestResponse response = client.Get(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
