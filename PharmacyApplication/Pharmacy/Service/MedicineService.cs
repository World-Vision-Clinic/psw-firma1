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

        public bool AddMedicine(Medicine medinice)
        {
            return repository.AddMedicine(medinice);
        }

        public bool DeleteMedicine(long medicineId)
        {
            return repository.DeleteMedicine(medicineId);
        }

        public bool UpdateMedicine(Medicine medicine)
        {
            return repository.UpdateMedicine(medicine);
        }
    }
}
