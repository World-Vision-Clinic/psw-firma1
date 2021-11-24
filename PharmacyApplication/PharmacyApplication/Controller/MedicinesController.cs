using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using Renci.SshNet;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        MedicineService service = new MedicineService(new MedicineRepository());
        HospitalsService hospitalService = new HospitalsService(new HospitalsRepository());
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());

        [HttpGet("check")]
        public IActionResult CheckMedicineAvailability(string name = "", string dosage = "", string quantity = "")
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

            if (name.Length <= 0 || dosage.Length <= 0 || quantity.Length <= 0)
            {
                return BadRequest();
            }

            double dosageInMg = 0;
            int quantityInBoxes = 0;
            try
            {
                dosageInMg = Double.Parse(dosage);
                quantityInBoxes = Int32.Parse(quantity);
            }
            catch
            {
                return BadRequest();
            }


            if (!service.CheckQuantity(name, dosageInMg, quantityInBoxes))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("OrderMedicine")]
        public IActionResult OrderMedicine(OrderingMedicineDto dto)
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

            if (!dto.Test)
            {
                Medicine medicine = new Medicine(dto.MedicineName, Double.Parse(dto.MedicineGrams), int.Parse(dto.NumOfBoxes));
                Medicine med = service.FoundOrderedMedicine(medicine);
                service.OrderMedicine(medicine);

                var client = new RestSharp.RestClient(hospital.Localhost);
                var request = new RestRequest("/medicines/ordered");
                request.AddHeader("Content-Type", "application/json");
                List<string> replacements = service.FoundReplacements(medicine);
                request.AddJsonBody(
                new
                {
                    MedicineName = med.MedicineName,
                    Manufacturer = med.Manufacturer,
                    SideEffects = med.SideEffects,
                    Usage = med.Usage,
                    Weigth = med.Weigth,
                    MainPrecautions = med.MainPrecautions,
                    PotentialDangers = med.PotentialDangers,
                    Quantity = dto.NumOfBoxes,
                    Replacements = replacements,
                    Price = med.Price
                });
                IRestResponse response = client.Post(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok();
                }
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("test")]
        public IActionResult TestingController()
        {
            return Ok("Hello from Medicine controller");
        }

        [HttpGet]
        public IActionResult GetConsumptionNotification()
        {
            //FileStream fs = medicineService.GetConsumedMedicinesInPeriodFile(dto.Beginning, dto.End);
            //UploadFile(fs);

            return Ok();
        }

        [HttpGet("medicineConsumation")]
        public IActionResult GetMedicineCousumation()
        {
            LoadFile();
            String consumationReport = ReadConsumationReport();
            return Ok(consumationReport);
        }

        public void LoadFile()
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", "user", "password")))
            {
                client.Connect();
                string serverFile = @"\public\consumed-medicine.txt";
                string localFile = "consumed-medicine.txt";
                using (Stream stream = System.IO.File.OpenWrite(localFile))
                {
                    client.DownloadFile(serverFile, stream, x => { Console.WriteLine(x); });
                }
                client.Disconnect();
            }
        }
        public String ReadConsumationReport()
        {
            StreamReader reader = new StreamReader("consumed-medicine.txt");
            String consumationReport = reader.ReadToEnd();
            reader.Close();
            return consumationReport;
        }
    }
}
