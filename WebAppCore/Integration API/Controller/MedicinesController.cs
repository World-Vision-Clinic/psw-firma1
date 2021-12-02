using Grpc.Core;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Services;
using Integration;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration_API.Mapper;
using IntegrationAPI.Protos;
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

        private SftpHandler sftpHandler = new SftpHandler();
        private IPharmacyConnection pharmacyConnection;
        
        public MedicinesController(IPharmacyConnection connection)
        {
            pharmacyConnection = connection;
        }

        [HttpPost("sendConsumptionNotification")]
        public IActionResult SendConsumptionNotification(MedicineConsumptionDto dto)
        {
            medicineService.CreateConsumedMedicinesInPeriodFile(dto.Beginning, dto.End);
            sftpHandler.UploadFile();


            return Ok();
        }

        [HttpGet("check")]
        public IActionResult CheckMedicineAvailability(string name = "", string dosage = "", string quantity = "")
        {
            if(name == null || dosage == null || quantity == null)
            {
                return BadRequest();
            }
            if (name.Length <= 0 || dosage.Length <= 0 || quantity.Length <= 0)
            {
                return BadRequest();
            }

            MedicineDto medicineDto = new MedicineDto { Name = name, DosageInMg = Double.Parse(dosage), Quantity = Int32.Parse(quantity) };
           
            List<PharmacyDto> pharmaciesWithMedicine = new List<PharmacyDto>();

            foreach(PharmacyProfile pharmacy in pharmaciesService.GetAll())
            {
                if (pharmacy.Protocol.Equals(ProtocolType.HTTP))
                {
                    if (pharmacyConnection.SendRequestToCheckAvailability(pharmacy.Localhost, medicineDto))
                    {
                        pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
                    }
                }
                else
                {
                    if (SendRequestToCheckAvailabilityGrpc(pharmacy.Localhost, medicineDto))
                    {
                        pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
                    }
                }
            }

            return Ok(pharmaciesWithMedicine);
        }
        public bool SendRequestToCheckAvailabilityGrpc(string pharmacyLocalhost, MedicineDto medicineDto)
        {
            Credential credential = credentialsService.GetByPharmacyLocalhost(pharmacyLocalhost);

            var input = new CheckMedicineExistenceRequest { MedicineName = medicineDto.Name, MedicineDosage = medicineDto.DosageInMg, Quantity = medicineDto.Quantity, ApiKey = credential.ApiKey };
            var channel = new Channel(pharmacyLocalhost, ChannelCredentials.Insecure);
            var client = new gRPCService.gRPCServiceClient(channel);
            var reply = client.checkMedicineExistenceAsync(input);
            if (reply.ResponseAsync.Result.Response.Equals("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SendMedicineOrderingRequestHTTP(OrderingMedicineDTO dto, bool test)
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

        [HttpPost("ordered")]
        public IActionResult OrderedHTTP(OrderedMedicineDTO dto)
        {
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
            orderedMedicine = new Medicine(Hospital.MedicalRecords.Service.Generator.GenerateMedicineId(), dto.MedicineName, Double.Parse(dto.Weigth), int.Parse(dto.Quantity), dto.Price, dto.Usage, null, dto.Replacements);
            ms.AddOrderedMedicine(orderedMedicine);
            return Ok();
        }

        [HttpPut("OrderMedicine")]
        public IActionResult Order(OrderingMedicineDTO dto)
        {
            if (pharmaciesService.Get(dto.Localhost).Protocol.Equals(ProtocolType.HTTP))
            {
                if (SendMedicineOrderingRequestHTTP(dto, false))
                {
                    return Ok();
                }
                return BadRequest();
            }
            else
            {
                if (SendMedicineOrderingRequestGRPC(dto, false))
                {
                    return Ok();
                }
                return BadRequest();
            }
        }

        public bool SendMedicineOrderingRequestGRPC(OrderingMedicineDTO dto, bool test)
        {
            double medicineGrams;
            int numOfBoxes;
            Credential credential = credentialsService.GetByPharmacyLocalhost(dto.Localhost);
            if (credential == null)
            {
                return false;
            }
            try
            {
                medicineGrams = Double.Parse(dto.MedicineGrams);
                numOfBoxes = int.Parse(dto.NumOfBoxes);
            }
            catch
            {
                return false;
            }

            var input = new MedicineOrderingRequest { MedicineName = dto.MedicineName, MedicineDosage = medicineGrams, Quantity = numOfBoxes, ApiKey = credential.ApiKey, Test = test };
            var channel = new Channel(dto.Localhost, ChannelCredentials.Insecure);
            var client = new gRPCService.gRPCServiceClient(channel);
            var reply = client.orderMedicineAsync(input);

            if (reply.ResponseAsync.Result.Response.Equals("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet("spec")]
        public IActionResult GetSpecification(string pharmacyLocalhost = "", string medicine = "")
        {
            if (pharmacyLocalhost.Length <= 0 || medicine.Length <= 0)
            {
                return BadRequest();
            }

            if (!pharmacyConnection.SendRequestForSpecification(pharmacyLocalhost, medicine))
            {
                return BadRequest("Specification does not exists");
            }

            if (!sftpHandler.DownloadSpecification($"/public/Specification.pdf"))
            {
                return BadRequest("Unable to download specification file");
            }

            return Ok();
        }
    }
}
