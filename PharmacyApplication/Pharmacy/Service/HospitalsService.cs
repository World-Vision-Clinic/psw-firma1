using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class HospitalsService
    {
        IHospitalsRepository repository;

        public HospitalsService(IHospitalsRepository hospitalsRepository)
        {
            repository = hospitalsRepository;
        }

        public List<Hospital> GetAll()
        {
            return repository.GetAll();
        }

        public Hospital GetHospitalByApiKey(string apiKey)
        {
            foreach (Hospital hospital in repository.GetAll())
            {
                if (apiKey.Equals(hospital.Key))
                {
                    return hospital;
                }
            }

            return null;
        }

        public bool AddNewHospital(Hospital newHospital)
        {
            foreach (Hospital hospital in GetAll())
            {
                if (hospital.Localhost.Equals(newHospital.Localhost))
                {
                    return false;
                }
            }
            repository.Save(newHospital);
            return true;
        }
    }
}
