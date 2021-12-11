using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Services;
using Hospital_API.Dto;
using Hospital_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private MedicineService medicineService = new MedicineService(new MedicinesRepository(), new MedicalRecordsRepository(), new ExaminationRepository());
        [HttpPost("ordered")]
        public IActionResult OrderedHTTP(OrderedMedicineDTO dto)
        {
            System.Diagnostics.Debug.WriteLine(dto.MedicineName + "asdaad");
            MedicineService ms = new MedicineService(new MedicinesRepository(), new MedicalRecordsRepository(), new ExaminationRepository());
            Medicine orderedMedicine;
            foreach (Medicine med in ms.GetAll())
            {
                if (med.Name.Equals(dto.MedicineName) && med.DosageInMg.Equals(dto.Weigth))
                {
                    orderedMedicine = new Medicine(med.ID, dto.MedicineName, Double.Parse(dto.Weigth), int.Parse(dto.Quantity), dto.Price, dto.MainPrecautions, null, dto.Replacements);
                    ms.AddOrderedMedicine(orderedMedicine);
                    return Ok();
                }
            }
            orderedMedicine = new Medicine(Hospital.MedicalRecords.Service.Generator.GenerateMedicineId(), dto.MedicineName, Double.Parse(dto.Weigth), int.Parse(dto.Quantity), dto.Price, dto.Usage, null, dto.Replacements);
            ms.AddOrderedMedicine(orderedMedicine);
            return Ok();
        }

        [HttpPost("sendConsumptionNotification")]
        public IActionResult SendConsumptionNotification(MedicineConsumptionDto dto)
        {
            medicineService.CreateConsumedMedicinesInPeriodFile(dto.Beginning, dto.End);

            return Ok();
        }
    }
}
