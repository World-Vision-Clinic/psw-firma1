using Grpc.Core;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Services;
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
        public override Task<SentOrderedMedicineResponse> addOrderedMedicine(SentOrderedMedicineRequest request, ServerCallContext context)
        {
            SentOrderedMedicineResponse response = new SentOrderedMedicineResponse();
            MedicineService ms = new MedicineService(new MedicinesRepository(), new MedicalRecordsRepository(), new ExaminationRepository());
            Medicine orderedMedicine;
            List<string> replacements = new List<string>();
            foreach(String replacement in request.Replacements)
            {
                replacements.Add(replacement);
            }
            foreach (Medicine med in ms.GetAll())
            {
                if (med.Name.Equals(request.MedicineName) && med.DosageInMg.Equals(request.Weight))
                {
                    orderedMedicine = new Medicine(med.ID, request.MedicineName, request.Weight, (int)request.Quantity, request.Price, request.MainPrecautions, null, replacements);
                    ms.AddOrderedMedicine(orderedMedicine);
                    response.Response = "OK";
                    return Task.FromResult(response);
                }
            }
            orderedMedicine = new Medicine(Hospital.MedicalRecords.Service.Generator.GenerateMedicineId(), request.MedicineName, request.Weight, (int)request.Quantity, request.Price, request.Usage, null, replacements);
            ms.AddOrderedMedicine(orderedMedicine);
            response.Response = "OK";
            return Task.FromResult(response);
        }
    }
}
