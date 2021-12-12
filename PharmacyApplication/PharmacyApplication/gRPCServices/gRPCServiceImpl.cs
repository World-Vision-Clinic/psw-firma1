using Grpc.Core;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyApi.Protos;
using PharmacyAPI.Mapper;
using PharmacyAPI.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.gRPCServices
{
    public class gRPCServiceImpl : gRPCService.gRPCServiceBase
    {
        HospitalsService hospitalsService = new HospitalsService(new HospitalsRepository()); 
        MedicineService service = new MedicineService(new MedicineRepository());
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());
        public const string PHARMACY_NAME = "Jankovic";
        public const string PHARMACY_URL = "127.0.0.1:5000";
        public override Task<RegisterPharmacyResponse> registerPharmacy(RegisterPharmacyRequest request, ServerCallContext context)
        {
            Credential newCredential = CredentialMapper.CredentialDtoToCredential(request.HospitalName, request.HospitalLocalhost, request.ApiKey);
            RegisterPharmacyResponse response = new RegisterPharmacyResponse();
            if (!credentialsService.AddNewCredential(newCredential))
            {
                response.Response = "Already exists!";
                return Task.FromResult(response);
            }

            string generatedKey = Generator.GenerateApiKey();

            var input = new RegisterHospitalRequest { PharmacyName = PHARMACY_NAME, PharmacyLocalhost = PHARMACY_URL, ApiKey = generatedKey };
            var channel = new Channel(request.HospitalLocalhost, ChannelCredentials.Insecure);
            var client = new gRPCHospitalService.gRPCHospitalServiceClient(channel);

            var reply = client.registerHospitalAsync(input);

            if(!reply.ResponseAsync.Result.Response.Equals("OK"))
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }

            Hospital newHospital = new Hospital { Localhost = request.HospitalLocalhost, Name = request.HospitalName, Key = generatedKey };
            newHospital.Key = generatedKey;
            if (!hospitalsService.AddNewHospital(newHospital))
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }
            response.Response = "OK";
            return Task.FromResult(response);
        }
        public override Task<CheckMedicineExistenceResponse> checkMedicineExistence(CheckMedicineExistenceRequest request, ServerCallContext context)
        {
            CheckMedicineExistenceResponse response = new CheckMedicineExistenceResponse();
            string apiKey = request.ApiKey;
            if(apiKey == null)
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }

            Hospital hospital = hospitalsService.GetHospitalByApiKey(apiKey);
            if (hospital == null)
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }

            if (request.MedicineName.Length <= 0)
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }

            double dosageInMg = 0;
            int quantityInBoxes = 0;
            try
            {
                dosageInMg = request.MedicineDosage;
                quantityInBoxes = (int)request.Quantity;
            }
            catch
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }


            if (!service.CheckQuantity(request.MedicineName, dosageInMg, quantityInBoxes))
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }

            response.Response = "OK";
            return Task.FromResult(response);
        }

        public override Task<MedicineOrderingResponse> orderMedicine(MedicineOrderingRequest request, ServerCallContext context)
        {
            string apiKey = request.ApiKey;
            MedicineOrderingResponse response = new MedicineOrderingResponse();
            if (apiKey == null)
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }
            Hospital hospital = hospitalsService.GetHospitalByApiKey(apiKey);
            if (hospital == null)
            {
                response.Response = "BAD REQUEST";
                return Task.FromResult(response);
            }
            if (!request.Test)
            {
                Medicine medicine = new Medicine(request.MedicineName, request.MedicineDosage, (int)request.Quantity);
                List<string> replacements = service.FoundReplacements(medicine);
                Medicine med = service.FoundOrderedMedicine(medicine);
                service.OrderMedicine(medicine);

                var input = new SentOrderedMedicineRequest { MedicineName = med.MedicineName, Manufacturer = med.Manufacturer, SideEffects = med.SideEffects, Usage = med.Usage, Weight = med.Weigth, PotentialDangers = med.PotentialDangers, Quantity = request.Quantity, Replacements = { replacements }, Price = med.Price};
                var channel = new Channel(hospital.Localhost, ChannelCredentials.Insecure);
                var client = new gRPCHospitalService.gRPCHospitalServiceClient(channel);
                var reply = client.addOrderedMedicineAsync(input);

                if (reply.ResponseAsync.Result.Response.Equals("OK"))
                {
                    response.Response = "OK";
                    return Task.FromResult(response);
                }
                else
                {
                    response.Response = "BAD REQUEST";
                    return Task.FromResult(response);
                }
            }
            else
            {
                response.Response = "OK";
                return Task.FromResult(response);
            }
        }
    }
}
