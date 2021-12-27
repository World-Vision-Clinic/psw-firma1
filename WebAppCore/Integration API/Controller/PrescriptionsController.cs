using System;
using Grpc.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration.Pharmacy.Model;
using Integration_API.Mapper;
using IntegrationAPI.Protos;
using RestSharp;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        private SftpHandler sftpHandler = new SftpHandler();
        private CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());
        private IPharmacyConnection pharmacyConnection;

        public PrescriptionsController(IPharmacyConnection connection)
        {
            pharmacyConnection = connection;
        }

        [HttpPost("sendPrescription")]
        public IActionResult SendPrescriptionToPharmacy(PrescriptionDto dto)
        {
            //metoda iz drugog kontrolera
            //pharmaciesWithMedicine = CheckMedicineAvailability(dto.MedicineName, dto.DosageInMg, dto.Quantity);
            PdfGenerator generator = new PdfGenerator();
            String filename = "Prescription" + dto.PatientName+".pdf";
            generator.GeneratePrescriptionPdf(filename,dto);
            
            List<PharmacyDto> pharmaciesWithMedicine = GetPharmaciesWithAvailableMedicine(dto.MedicineName, dto.DosageInMg, dto.Quantity);
            if (pharmaciesWithMedicine.Count == 0)
                return Ok("No pharmacies with specified medicine found");
            bool isSend = SendPrescription(pharmaciesWithMedicine, filename);
            if (!isSend)
                return Ok("Cannot send prescription to pharmacie, their server is not working");

            return Ok("Prescription sent to pharmacy ");
        }

        public bool SendPrescription(List<PharmacyDto> pharmaciesWithMedicine, String filename)
        {
            bool isSend = false;
            foreach (var pharmacy in pharmaciesWithMedicine)
            {
                if (pharmacy.Protocol == ProtocolType.HTTP)
                {
                    isSend = sendPdfFileviaHttp(filename, pharmacy);
                }
                else
                {
                    SftpHandler stfp = new SftpHandler();
                    stfp.UploadPdfFile(filename);
                    isSend = true;
                }
                if (isSend)
                    return true;
            }

            return false;
        }

        public bool NotifyPharmacyAboutSendFile(PharmacyDto dto, String filename)
        {
            var client = new RestSharp.RestClient(dto.Localhost);
            var request = new RestRequest("prescriptions/DownloadPrescription");

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
                FileName = filename
            }) ;
            IRestResponse response = client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool sendPdfFileviaHttp(String filename, PharmacyDto dto)
        {
            var client = new RestSharp.RestClient(dto.Localhost);
            var request = new RestRequest("prescriptions/DownloadQRPrescription");

            Credential credential = credentialsService.GetByPharmacyLocalhost(dto.Localhost);

            if (credential == null)
            {
                return false;
            }
            request.AddHeader("ApiKey", credential.ApiKey);
            request.AddHeader("Content-Type", "application/json");

            Byte[] biti = System.IO.File.ReadAllBytes(filename);
            String file = Convert.ToBase64String(biti);

            request.AddHeader("fileName", filename);
            //request.AddJsonBody(file);
            //request.AddFile(filename,filename, filename);
            request.AddJsonBody(
            new
            {
                File = file
            });
            IRestResponse response = client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        /*public void SendUsingHttp(string fileName)
        {
            var client = new RestClient(pharmacyUrl);
            var request = new RestRequest();

            Byte[] biti = File.ReadAllBytes(fileName);
            String file = Convert.ToBase64String(biti);

            request.AddHeader("fileName", "Prescription2.pdf");
            request.AddJsonBody(file);
            client.Post(request);
        }*/

        public List<PharmacyDto> GetPharmaciesWithAvailableMedicine(string name = "", string dosage = "", string quantity = "")
        {

            MedicineDto medicineDto = new MedicineDto { Name = name, DosageInMg = Double.Parse(dosage), Quantity = Int32.Parse(quantity) };

            List<PharmacyDto> pharmaciesWithMedicine = new List<PharmacyDto>();

            foreach (PharmacyProfile pharmacy in pharmaciesService.GetAll())
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

            return pharmaciesWithMedicine;
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
    }
}