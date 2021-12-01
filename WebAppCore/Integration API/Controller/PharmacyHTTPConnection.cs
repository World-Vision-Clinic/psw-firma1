using Grpc.Core;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using IntegrationAPI.Protos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using IntegrationAPI.Protos;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public class PharmacyHTTPConnection: IPharmacyConnection
    {
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());

        public bool SendRequestToCheckAvailability(string pharmacyLocalhost, MedicineDto medicineDto)
        {
            var client = new RestSharp.RestClient(pharmacyLocalhost);
            var request = new RestRequest("/medicines/check?name=" + medicineDto.Name + "&dosage=" + medicineDto.DosageInMg + "&quantity=" + medicineDto.Quantity);

            Credential credential = credentialsService.GetByPharmacyLocalhost(pharmacyLocalhost);

            request.AddHeader("ApiKey", credential.ApiKey);

            IRestResponse response = client.Get(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        

        public bool SendRequestForSpecification(string pharmacyLocalhost, string medicineName)
        {
            var client = new RestSharp.RestClient(pharmacyLocalhost);
            var request = new RestRequest("/medicines/spec?name=" + medicineName);

            Credential credential = credentialsService.GetByPharmacyLocalhost(pharmacyLocalhost);

            request.AddHeader("ApiKey", credential.ApiKey);

            IRestResponse response = client.Get(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
