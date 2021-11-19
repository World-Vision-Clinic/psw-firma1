using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
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


            if(!service.CheckQuantity(name, dosageInMg, quantityInBoxes))
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

            if (!dto.Test) {
                Medicine medicine = new Medicine(dto.MedicineName, Double.Parse(dto.MedicineGrams), int.Parse(dto.NumOfBoxes));
                service.OrderMedicine(medicine);

                var client = new RestSharp.RestClient(hospital.Localhost);
                var request = new RestRequest("/medicines/ordered");
                Medicine med = service.FoundOrderedMedicine(medicine);
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
    }
}
