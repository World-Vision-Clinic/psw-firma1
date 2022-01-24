using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class PrescriptionMedicineDTO
    {
        public string MedicineName { get; set; }
        public int DurationInDays { get; set; }
        public int TimesPerDay { get; set; }
        public string Quantity { get; set; }

        public PrescriptionMedicineDTO() { }
        public PrescriptionMedicineDTO(string medicineName, int durationInDays, int timesPerDay, string quantity)
        {
            MedicineName = medicineName;
            DurationInDays = durationInDays;
            TimesPerDay = timesPerDay;
            Quantity = quantity;
        }
        public PrescriptionMedicineDTO(PrescriptionMedicine prescriptionMedicine, Medicine medicine)
        {
            DurationInDays = prescriptionMedicine.DurationInDays;
            TimesPerDay = prescriptionMedicine.TimesPerDay;
            Quantity = prescriptionMedicine.Quantity;
            MedicineName = medicine.Name;
        }
    }
}
