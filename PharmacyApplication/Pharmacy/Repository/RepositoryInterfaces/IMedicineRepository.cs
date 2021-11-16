using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface IMedicineRepository
    {
        public List<Medicine> GetAll();
        Medicine GetById(long medicineId);
        public void AddMedicine(Medicine medinice);
        public void DeleteMedicine(long medicineId);
        public void UpdateMedicine(Medicine medicine);
    }
}
