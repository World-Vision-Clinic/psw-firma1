using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public class PrescriptionMedicine : Entity
    {
        public int AppointmentId { get; set; }
        public string MedicineId { get; set; }
        public int DurationInDays { get; set; }
        public int TimesPerDay { get; set; }
        public string Quantity { get; set; }

        public PrescriptionMedicine() { }
        public PrescriptionMedicine(int appointmentId, string medicineId, int durationInDays, int timesPerDay, string quantity)
        {
            AppointmentId = appointmentId;
            MedicineId = medicineId;
            DurationInDays = durationInDays;
            TimesPerDay = timesPerDay;
            Quantity = quantity;
        }
    }
}
