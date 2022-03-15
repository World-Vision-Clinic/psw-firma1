using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using RestSharp;
using System;

namespace Integration_API.Controller
{
    public class PharmacyHTTPConnection: IPharmacyHttpConnection
    {
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());
        public const string HOSPITAL_NAME = "World Vision Clinic";
        public const string HOSPITAL_URL = "http://localhost:43818";
        public const string HOSPITAL_PORT = "127.0.0.1:3000";
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

        public bool SendMedicineOrderingRequestHTTP(OrderingMedicineDTO dto)
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
                NumOfBoxes = dto.NumOfBoxes
            });
            IRestResponse response = client.Post(request);
            return response.StatusCode.Equals(System.Net.HttpStatusCode.OK);
        }

        public bool SendRegistrationRequestHttp(PharmacyDto dto, string generatedKey)
        {
            var client = new RestSharp.RestClient(dto.Localhost);
            var request = new RestRequest("/credentials");

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
            new
            {
                HospitalName = HOSPITAL_NAME,
                HospitalLocalhost = HOSPITAL_URL,
                ApiKey = generatedKey
            });

            IRestResponse response = client.Post(request);  // POST /credential  {"Name": "World Vision Clinic", "HospitalLocalhost": "http://localhost:43818", "ApiKey": "wqhegyqwegqyw21543"}
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return false;
            return true;
        }

        public bool NotifyPharmacyAboutSendFile(PharmacyDto dto, string filename)
        {
            var client = new RestClient(dto.Localhost);
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
            });
            IRestResponse response = client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool sendPdfFileviaHttp(string filename, PharmacyDto dto)
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

            byte[] biti = System.IO.File.ReadAllBytes(filename);
            string file = Convert.ToBase64String(biti);

            request.AddHeader("fileName", filename);
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
    }
}
