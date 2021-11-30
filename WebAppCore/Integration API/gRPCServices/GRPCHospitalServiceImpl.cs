using Grpc.Core;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Mapper;
using IntegrationAPI.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.gRPCServices
{
    public class GRPCHospitalServiceImpl : gRPCHospitalService.gRPCHospitalServiceBase
    {
        CredentialsService service = new CredentialsService(new CredentialsRepository());

        public override Task<RegisterHospitalResponse> registerHospital(RegisterHospitalRequest request, ServerCallContext context)
        {
            Credential newCredential = CredentialMapper.CredentialDtoToCredential(request.PharmacyName, request.PharmacyLocalhost, request.ApiKey);
            RegisterHospitalResponse response = new RegisterHospitalResponse();
            if (!service.AddNewCredential(newCredential))
            {
                response.Response = "Already exists!";

                return Task.FromResult(response);
            }

            response.Response = "OK";
            return Task.FromResult(response);
        }
    }
}
