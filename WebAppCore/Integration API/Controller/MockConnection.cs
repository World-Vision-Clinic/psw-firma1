using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public class MockConnection : IPharmacyHttpConnection, IPharmacyGrpcConnection
    {
        public bool SendRequestToCheckAvailability(string pharmacyLocalhost, MedicineDto medicineDto)
        {
            return medicineDto.Name.Equals("Aspirin") && medicineDto.DosageInMg == 200 && medicineDto.Quantity <= 5;
        }
        public bool SendRequestForSpecification(string pharmacyLocalhost, string medicineName)
        {
            return medicineName.Equals("Aspirin");
        }

        public bool SendRequestToCheckAvailabilityGrpc(string pharmacyLocalhost, MedicineDto medicineDto)
        {
            return medicineDto.Name.Equals("Aspirin") && medicineDto.DosageInMg == 200 && medicineDto.Quantity <= 5;
        }

        public bool SendMedicineOrderingRequestHTTP(OrderingMedicineDTO dto, bool test)
        {
            return true;
        }

        public bool sendPdfFileviaHttp(string filename, PharmacyDto dto)
        {
            return true;
        }

        public bool SendMedicineOrderingRequestHTTP(OrderingMedicineDTO dto)
        {
            throw new NotImplementedException();
        }

        public bool SendMedicineOrderingRequestGRPC(OrderingMedicineDTO dto)
        {
            throw new NotImplementedException();
        }

        public bool SendRegistrationRequestGrpc(PharmacyDto dto, string generatedKey)
        {
            throw new NotImplementedException();
        }
    }
}
