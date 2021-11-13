using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface IHospitalsRepository
    {
        public List<Hospital> GetAll();
        public void Save(Hospital hospitalProfile);
    }
}
