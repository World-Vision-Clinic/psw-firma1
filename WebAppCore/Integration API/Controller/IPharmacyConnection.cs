using Integration.Pharmacy.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public interface IPharmacyConnection
    {
        bool SendRequestToCheckAvailability(string pharmacyLocalhost, MedicineDto medicineDto);
    }
}
