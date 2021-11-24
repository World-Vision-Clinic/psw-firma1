﻿using Integration;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration.Services;
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
        private MedicineService medicineService = new MedicineService(new MedicinesRepository(), new MedicalRecordsRepository(), new ExaminationRepository());

        [HttpPost("sendConsumptionNotification")]
        public IActionResult SendConsumptionNotification(MedicineConsumptionDto dto)
        {
            medicineService.CreateConsumedMedicinesInPeriodFile(dto.Beginning, dto.End);
            UploadFile();


            return Ok();
        }

        public void UploadFile()
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", "user", "password")))
            {
                client.Connect();
                string sourceFile = @"consumed-medicine.txt";
                using (Stream stream = System.IO.File.OpenRead(sourceFile))
                {
                    client.UploadFile(stream, @"\public\" + Path.GetFileName(sourceFile), x => { Console.WriteLine(x); });
                }
                client.Disconnect();
            }
        }

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
                if (SendRequest(pharmacy, medicineDto))
                {
                    pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
                }
            }

            return Ok(pharmaciesWithMedicine);
        }

        private bool SendRequest(PharmacyProfile pharmacy, MedicineDto medicineDto)
        {
            var client = new RestSharp.RestClient(pharmacy.Localhost);
            var request = new RestRequest("/medicines/check?name=" + medicineDto.Name + "&dosage=" + medicineDto.DosageInMg + "&quantity=" + medicineDto.Quantity);

            Credential credential = credentialsService.GetByPharmacyLocalhost(pharmacy.Localhost);

            if(credential == null)
            {
                return false;
            }

            request.AddHeader("ApiKey", credential.ApiKey);

            IRestResponse response = client.Get(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool SendMedicineOrderingRequest(OrderingMedicineDTO dto, bool test)
        {

            var client = new RestSharp.RestClient(dto.Localhost);
            var request = new RestRequest("medicines/OrderMedicine");

            Credential credential = credentialsService.GetByPharmacyLocalhost(dto.Localhost);

            if (credential == null)
            {
                return false;
            }
            request.AddHeader("ApiKey", credential.ApiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
            new
            {
                MedicineName = dto.MedicineName,
                MedicineGrams = dto.MedicineGrams,
                NumOfBoxes = dto.NumOfBoxes,
                Test = test
            });
            IRestResponse response = client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        [HttpPut("OrderMedicine")]
        public IActionResult Order(OrderingMedicineDTO dto)
        {
            if (SendMedicineOrderingRequest(dto, false))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("ordered")]
        public IActionResult Ordered(OrderedMedicineDTO dto)
        {
            System.Diagnostics.Debug.WriteLine(dto.Replacements);
            MedicineService ms = new MedicineService(new MedicinesRepository(), new MedicalRecordsRepository(), new ExaminationRepository());
            Medicine orderedMedicine;
            foreach (Medicine med in ms.GetAll())
            {
                if (med.Name.Equals(dto.MedicineName) && med.DosageInMg.Equals(dto.Weigth))
                {
                    orderedMedicine = new Medicine(med.ID, dto.MedicineName, Double.Parse(dto.Weigth), int.Parse(dto.Quantity), dto.Price, dto.MainPrecautions, null, dto.Replacements);
                    ms.AddOrderedMedicine(orderedMedicine);
                    return Ok();
                }
            }
            orderedMedicine = new Medicine(Generator.GenerateMedicineId(), dto.MedicineName, Double.Parse(dto.Weigth), int.Parse(dto.Quantity), dto.Price, dto.Usage, null, dto.Replacements);
            ms.AddOrderedMedicine(orderedMedicine);
            return Ok();
        }
         

    }
}
