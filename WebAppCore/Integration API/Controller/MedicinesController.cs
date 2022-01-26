using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RestSharp;
using System;
using System.Collections.Generic;
using File = Integration.Pharmacy.Model.File;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        private FilesService filesService = new FilesService(new FilesRepository());
        private PharmacyGRPConnection gRPConnection = new PharmacyGRPConnection();

        private SftpHandler sftpHandler = new SftpHandler();
        private IPharmacyConnection httpConnection;
        public const string HOSPITAL_URL = "http://localhost:39901";

        private readonly IHubContext<SignalServer> _hubContext;

        public MedicinesController(IPharmacyConnection connection, IHubContext<SignalServer> hubcontext)
        {
            httpConnection = connection;
            _hubContext = hubcontext;
        }

        [HttpGet("check")]
        public IActionResult CheckMedicineAvailability(string name = "", string dosage = "", string quantity = "")
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(dosage) || string.IsNullOrEmpty(quantity))
                return BadRequest("Please fill all fields");

            MedicineDto medicineDto = new MedicineDto { Name = name, DosageInMg = Double.Parse(dosage), Quantity = Int32.Parse(quantity) };

            List<PharmacyDto> pharmaciesWithMedicine = new List<PharmacyDto>();
            foreach (PharmacyProfile pharmacy in pharmaciesService.GetAll())
            {
                if (pharmacy.ConnectionInfo.Protocol.Equals(ProtocolType.HTTP))
                {
                    if (httpConnection.SendRequestToCheckAvailability(pharmacy.ConnectionInfo.Domain, medicineDto))
                        pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
                }
                else
                {
                    if (gRPConnection.SendRequestToCheckAvailabilityGrpc(pharmacy.ConnectionInfo.Domain, medicineDto))
                        pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
                }
            }

            return Ok(pharmaciesWithMedicine);
        }

        [HttpPost("ordered")]
        public IActionResult OrderedHTTP(OrderedMedicineDTO dto)
        {
            var client = new RestClient(HOSPITAL_URL);
            var request = new RestRequest("/medicines/ordered");
            request.AddJsonBody(dto);
            IRestResponse response = client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok();

            return BadRequest();
        }

        [HttpPut("OrderMedicine")]
        public IActionResult Order(OrderingMedicineDTO dto)
        {
            if (pharmaciesService.Get(dto.Localhost).ConnectionInfo.Protocol.Equals(ProtocolType.HTTP))
            {
                if (httpConnection.SendMedicineOrderingRequestHTTP(dto, false))
                    return Ok();
                return BadRequest("Please try again, procurement wasn't executed");
            }
            else
            {
                if (gRPConnection.SendMedicineOrderingRequestGRPC(dto, false))
                    return Ok();
                return BadRequest("Please try again, procurement wasn't executed");
            }
        }

        [HttpGet("spec")]
        public IActionResult GetSpecification(string pharmacyLocalhost = "", string medicine = "")
        {
            if (string.IsNullOrEmpty(pharmacyLocalhost) || string.IsNullOrEmpty(medicine))
                return BadRequest();

            if (!httpConnection.SendRequestForSpecification(pharmacyLocalhost, medicine))
                return BadRequest("Specification does not exists");

            File dowloadedSpec = sftpHandler.DownloadSpecification($"/public/" + medicine + ".pdf", "Specifications/" + medicine + ".pdf");

            if (dowloadedSpec == null)
                return BadRequest("Unable to download specification file");

            filesService.UpdateSpecification(dowloadedSpec);
            _hubContext.Clients.All.SendAsync("askServerResponse", "You recieved file " + medicine + ".pdf");
            return Ok();
        }

        [HttpPost("sendConsumptionNotification")]
        public IActionResult SendConsumptionNotification(MedicineConsumptionDto dto)
        {
            var client = new RestClient(HOSPITAL_URL);
            var request = new RestRequest("/medicines/sendConsumptionNotification");
            request.AddJsonBody(
            new
            {
                Beginning = dto.Beginning,
                End = dto.End
            });

            IRestResponse response = client.Post(request);
            bool success = sftpHandler.UploadFile();
            if (!success)
                return BadRequest("Unable to upload consumption report.");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok();

            return BadRequest("Unable to send consumption notification.");
        }
    }
}
