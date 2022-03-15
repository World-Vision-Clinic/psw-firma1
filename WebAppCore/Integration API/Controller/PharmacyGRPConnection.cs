using Grpc.Core;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using IntegrationAPI.Protos;
using System;

namespace Integration_API.Controller
{
    public class PharmacyGRPConnection : IPharmacyGrpcConnection
    {

        private CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());
        public const string HOSPITAL_NAME = "World Vision Clinic";
        public const string HOSPITAL_URL = "http://localhost:43818";
        public const string HOSPITAL_PORT = "127.0.0.1:3000";
        public bool SendMedicineOrderingRequestGRPC(OrderingMedicineDTO dto)
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

            var input = new MedicineOrderingRequest { MedicineName = dto.MedicineName, MedicineDosage = medicineGrams, Quantity = numOfBoxes, ApiKey = credential.ApiKey};
            var channel = new Channel(dto.Localhost, ChannelCredentials.Insecure);
            var client = new gRPCService.gRPCServiceClient(channel);
            var reply = client.orderMedicineAsync(input);

            return reply.ResponseAsync.Result.Response.Equals("OK");
            
        }

        public bool SendRequestToCheckAvailabilityGrpc(string pharmacyLocalhost, MedicineDto medicineDto)
        {
            Credential credential = credentialsService.GetByPharmacyLocalhost(pharmacyLocalhost);
            var input = new CheckMedicineExistenceRequest { MedicineName = medicineDto.Name, MedicineDosage = medicineDto.DosageInMg, Quantity = medicineDto.Quantity, ApiKey = credential.ApiKey };
            var channel = new Channel(pharmacyLocalhost, ChannelCredentials.Insecure);
            var client = new gRPCService.gRPCServiceClient(channel);
            var reply = client.checkMedicineExistenceAsync(input);
            return reply.ResponseAsync.Result.Response.Equals("OK");
            
        }

        public bool SendRegistrationRequestGrpc(PharmacyDto dto, String generatedKey)
        {
            var input = new RegisterPharmacyRequest { HospitalName = HOSPITAL_NAME, HospitalLocalhost = HOSPITAL_PORT, ApiKey = generatedKey };
            var channel = new Channel(dto.Localhost, ChannelCredentials.Insecure);
            var client = new gRPCService.gRPCServiceClient(channel);
            var reply = client.registerPharmacyAsync(input);
            return reply.ResponseAsync.Result.Response.Equals("OK");
        }
    }
}

