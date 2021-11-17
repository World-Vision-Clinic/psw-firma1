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
        public bool AddMedicine(Medicine medinice);
        public bool DeleteMedicine(long medicineId);
        public bool UpdateMedicine(Medicine medicine);
    }
}
