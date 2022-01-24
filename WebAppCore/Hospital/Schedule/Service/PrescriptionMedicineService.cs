using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class PrescriptionMedicineService
    {
        private readonly IPrescriptionMedicineRepository _repo;

        public PrescriptionMedicineService(IPrescriptionMedicineRepository repo)
        {
            _repo = repo;
        }

        public void AddPrescriptionMedicine(PrescriptionMedicine prescriptionMedicine)
        {
            _repo.AddPrescriptionMedicine(prescriptionMedicine);
        }

        public PrescriptionMedicine FindById(int id)
        {
            return _repo.FindById(id);
        }

        public List<PrescriptionMedicine> FindByAppointmentId(int appointmentId)
        {
            return _repo.FindByAppointmentId(appointmentId);
        }
        public List<Medicine> FindMedicinesByAppointmentId(int appointmentId)
        {
            return _repo.FindMedicinesByAppointmentId(appointmentId);
        }
        public List<PrescriptionMedicine> FindByMedicineId(string medicineId)
        {
            return _repo.FindByMedicineId(medicineId);
        }
        public List<Appointment> FindAppointmentsByMedicineId(string medicineId)
        {
            return _repo.FindAppointmentsByMedicineId(medicineId);
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }

    }
}
