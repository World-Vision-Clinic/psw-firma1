using Grpc.Core;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Mapper;
using IntegrationAPI.Protos;
using RestSharp;
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
        public override Task<SentOrderedMedicineResponse> addOrderedMedicine(SentOrderedMedicineRequest request, ServerCallContext context)
        {
            SentOrderedMedicineResponse response = new SentOrderedMedicineResponse();
            var client = new RestSharp.RestClient("http://localhost:8080");
            var requestHttp = new RestRequest("medicines/ordered");
            List<string> replacements = new List<string>();
            foreach (String replacement in request.Replacements)
            {
                replacements.Add(replacement);
            }
            requestHttp.AddJsonBody(
            new
            {
                MedicineName = request.MedicineName,
                Manufaccturer = request.Manufacturer,
                SideEffects = request.SideEffects,
                Usage = request.Usage,
                Weigth = request.Weight,
                MainPrecautions = request.MainPrecautions,
                PotentialDangers = request.PotentialDangers,
                Quantity = request.Quantity,
                Replacements = replacements,
                Price = request.Price
            });
            IRestResponse responseHttp = client.Post(requestHttp);
            if (responseHttp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.Response = "OK";
                return Task.FromResult(response);
            }
            response.Response = "BAD REQUEST";
            return Task.FromResult(response);
        }
    }
}
