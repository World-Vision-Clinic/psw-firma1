using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IPrescriptionMedicineRepository
    {
        public void SaveSync();
        public PrescriptionMedicine FindById(int id);
        public List<Medicine> FindMedicinesByAppointmentId(int appointmentId);
        public List<Appointment> FindAppointmentsByMedicineId(string medicineId);
        public List<PrescriptionMedicine> FindByAppointmentId(int appointmentId);
        public List<PrescriptionMedicine> FindByMedicineId(string medicineId);
        public void AddPrescriptionMedicine(PrescriptionMedicine prescriptionMedicine);
    }
}
