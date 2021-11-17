using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class MedicineService
    {
        IMedicineRepository repository;

        public MedicineService(IMedicineRepository medicineRepository)
        {
            repository = medicineRepository;
        }

        public List<Medicine> GetAll() 
        {
            return repository.GetAll();
        }

        public Medicine GetById(long medicineId) 
        {
            return repository.GetById(medicineId);
        }

        public void AddMedicine(Medicine medinice)
        {
            repository.AddMedicine(medinice);
        }

        public void DeleteMedicine(long medicineId)
        {
            repository.DeleteMedicine(medicineId);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            repository.UpdateMedicine(medicine);
        }

        public bool ProcureMedicine(long medicineId, int quantity)
        {
            return repository.ProcureMedicine(medicineId, quantity);
        }
    }
}
