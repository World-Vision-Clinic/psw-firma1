using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public interface IPharmacyGrpcConnection
    {
        bool SendMedicineOrderingRequestGRPC(OrderingMedicineDTO dto);

        bool SendRequestToCheckAvailabilityGrpc(string pharmacyLocalhost, MedicineDto medicineDto);

        bool SendRegistrationRequestGrpc(PharmacyDto dto, String generatedKey);

    }
}
