using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface IHospitalProfilesRepository
    {
        public List<HospitalProfile> GetAll();
        public void Save(HospitalProfile hospitalProfile);
    }
}
