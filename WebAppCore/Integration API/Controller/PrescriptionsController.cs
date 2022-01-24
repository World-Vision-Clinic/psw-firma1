using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration.Pharmacy.Model;
using Integration_API.Mapper;
using System;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        private PharmacyGRPConnection grpcConnection = new PharmacyGRPConnection();
        private IPharmacyConnection pharmacyConnection;

        public PrescriptionsController(IPharmacyConnection connection)
        {
            pharmacyConnection = connection;
        }

        [HttpPost("sendPrescription")]
        public IActionResult SendPrescriptionToPharmacy(PrescriptionDto dto)
        {
            PdfGenerator generator = new PdfGenerator();
            string filename = "Prescription" + dto.PatientName+".pdf";
            generator.GeneratePrescriptionPdf(filename,dto);
            
            List<PharmacyDto> pharmaciesWithMedicine = GetPharmaciesWithAvailableMedicine(dto.MedicineName, dto.DosageInMg, dto.Quantity);
            if (pharmaciesWithMedicine.Count == 0)
                return BadRequest("No pharmacies with specified medicine found");

            if (!SendPrescription(pharmaciesWithMedicine, filename))
                return BadRequest("Cannot send prescription to pharmacie, their server is not working");

            return Ok("Prescription sent to pharmacy ");
        }

        public bool SendPrescription(List<PharmacyDto> pharmaciesWithMedicine, string filename)
        {
            bool isSend = false;
            foreach (var pharmacy in pharmaciesWithMedicine)
            {
                if (pharmacy.Protocol == ProtocolType.HTTP)
                    isSend = pharmacyConnection.sendPdfFileviaHttp(filename, pharmacy);
                else
                {
                    SftpHandler stfp = new SftpHandler();
                    stfp.UploadPdfFile(filename);
                    isSend = true;
                    break;
                }
            }

            return isSend;
        }

        public List<PharmacyDto> GetPharmaciesWithAvailableMedicine(string name = "", string dosage = "", string quantity = "")
        {
            MedicineDto medicineDto = new MedicineDto { Name = name, DosageInMg = Double.Parse(dosage), Quantity = Int32.Parse(quantity) };

            List<PharmacyDto> pharmaciesWithMedicine = new List<PharmacyDto>();
            foreach (PharmacyProfile pharmacy in pharmaciesService.GetAll())
            {
                if (pharmacy.ConnectionInfo.Protocol.Equals(ProtocolType.HTTP))
                    if (pharmacyConnection.SendRequestToCheckAvailability(pharmacy.ConnectionInfo.Domain, medicineDto))
                        pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
                else
                    if (grpcConnection.SendRequestToCheckAvailabilityGrpc(pharmacy.ConnectionInfo.Domain, medicineDto))
                        pharmaciesWithMedicine.Add(PharmacyMapper.PharmacyToPharmacyDto(pharmacy));
            }

            return pharmaciesWithMedicine;
        }
        
    }
}