using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class PrescriptionMedicineRepository : IPrescriptionMedicineRepository
    {
        private readonly HospitalContext _context;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicinesRepository _medicinesRepository;

        public PrescriptionMedicineRepository()
        {
        }

        public PrescriptionMedicineRepository(HospitalContext context, IAppointmentRepository appointmentRepository, IMedicinesRepository medicinesRepository)
        {
            _context = context;
            _appointmentRepository = appointmentRepository;
            _medicinesRepository = medicinesRepository;
        }

        public void SaveSync()
        {
            _context.SaveChanges();
        }

        public void AddPrescriptionMedicine(PrescriptionMedicine prescriptionMedicine)
        {
            _context.PrescriptionMedicines.Add(prescriptionMedicine);
            SaveSync();
        }

        public PrescriptionMedicine FindById(int id)
        {
            return _context.PrescriptionMedicines.FirstOrDefault(d => d.Id == id);
        }

        public List<PrescriptionMedicine> FindByAppointmentId(int appointmentId)
        {
            return _context.PrescriptionMedicines.Where(f => f.AppointmentId == appointmentId).ToList();
        }

        public List<PrescriptionMedicine> FindByMedicineId(string medicineId)
        {
            return _context.PrescriptionMedicines.Where(f => f.MedicineId == medicineId).ToList();
        }

        public List<Medicine> FindMedicinesByAppointmentId(int appointmentId)
        {
            List<string> medicineIds = _context.PrescriptionMedicines.Where(f => f.AppointmentId == appointmentId).Select(u => u.MedicineId).ToList();
            List<Medicine> medicines = _context.Medicines.Where(a => medicineIds.Contains(a.ID)).ToList();
            return medicines;
        }

        public List<Appointment> FindAppointmentsByMedicineId(string medicineId)
        {
            var query = from pa in _context.PrescriptionMedicines
                        where pa.MedicineId == medicineId
                        select new
                        {
                            appointmentId = pa.AppointmentId
                        };
            List<Appointment> appointments = new List<Appointment>();
            foreach (var a in query)
            {
                appointments.Add(_appointmentRepository.FindById(a.appointmentId));
            }
            return appointments;
        }
    }
}
