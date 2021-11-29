using Grpc.Core;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyApi.Protos;
using PharmacyAPI.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.gRPCServices
{
    public class gRPCServiceImpl : gRPCService.gRPCServiceBase
    {
        CredentialsService service = new CredentialsService(new CredentialsRepository());
        HospitalsService hospitalsService = new HospitalsService(new HospitalsRepository());
        public const string PHARMACY_NAME = "Jankovic";
        public const string PHARMACY_URL = "5000";
        public override Task<RegisterPharmacyResponse> registerPharmacy(RegisterPharmacyRequest request, ServerCallContext context)
        {
            Credential newCredential = CredentialMapper.CredentialDtoToCredential(request.HospitalName, request.HospitalLocalhost, request.ApiKey);
            RegisterPharmacyResponse response = new RegisterPharmacyResponse();
            if (!service.AddNewCredential(newCredential))
            {
                response.Response = "Already exists!";
                return Task.FromResult(response);
            }

            string generatedKey = Generator.GenerateApiKey();

            var input = new RegisterHospitalRequest { PharmacyName = PHARMACY_NAME, PharmacyLocalhost = PHARMACY_URL, ApiKey = generatedKey };
            var channel = new Channel("127.0.0.1:" + request.HospitalLocalhost, ChannelCredentials.Insecure);
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
    }
}
