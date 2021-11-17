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

        public List<Medicine> GetByName(string name)
        {
            List<Medicine> medicines = new List<Medicine>();

            foreach (Medicine m in repository.GetAll())
            {
                if (m.MedicineName.ToLower().Contains(name.ToLower()))
                {
                    medicines.Add(m);
                }
            }

            return medicines;

        }

        public List<Medicine> ConvertIdsToMedicines(List<long> ids)
        {
            List<Medicine> medicines = new List<Medicine>();

            foreach(long l in ids)
            {
                medicines.Add(GetById(l));
            }

            return medicines;
        }
    }
}
