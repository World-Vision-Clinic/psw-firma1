using Grpc.Core;
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
using File = Integration.Pharmacy.Model.File;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        private CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());
        private FilesService filesService = new FilesService(new FilesRepository());

        private SftpHandler sftpHandler = new SftpHandler();
        private IPharmacyConnection pharmacyConnection;
        public const string HOSPITAL_URL = "http://localhost:39901";

        public MedicinesController(IPharmacyConnection connection)
        {
            pharmacyConnection = connection;
        }

        [HttpPost("sendConsumptionNotification")]
        public IActionResult SendConsumptionNotification(MedicineConsumptionDto dto)
        {
            var client = new RestSharp.RestClient(HOSPITAL_URL);
            var request = new RestRequest("/medicines/sendConsumptionNotification");

            request.AddJsonBody(
            new
            {
                Beginning = dto.Beginning,
                End = dto.End
            });
            IRestResponse response = client.Post(request);
            
            sftpHandler.UploadFile();


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }

            return BadRequest();
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
            var client = new RestSharp.RestClient(HOSPITAL_URL);
            var request = new RestRequest("/medicines/ordered");

            request.AddJsonBody(
            new
            {
                MedicineName = dto.MedicineName,
                Manufaccturer = dto.Manufacturer,
                SideEffects = dto.SideEffects,
                Usage = dto.Usage,
                Weigth = dto.Weigth,
                MainPrecautions = dto.MainPrecautions,
                PotentialDangers = dto.PotentialDangers,
                Quantity = dto.Quantity,
                Replacements = dto.Replacements,
                Price = dto.Price
            });
            IRestResponse response = client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }

            return BadRequest();
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
                return false;
            
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

            File dowloadedSpec = sftpHandler.DownloadSpecification($"/public/" + medicine + ".pdf", "Specifications/" + medicine + ".pdf");

            if (dowloadedSpec == null)
            {
                return BadRequest("Unable to download specification file");
            }

            filesService.UpdateSpecification(dowloadedSpec);

            return Ok();
        }
    }
}
