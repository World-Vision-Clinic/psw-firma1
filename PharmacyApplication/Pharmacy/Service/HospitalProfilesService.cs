using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class HospitalProfilesService
    {
        IHospitalProfilesRepository repository;

        public HospitalProfilesService(IHospitalProfilesRepository hospitalProfilesRepository)
        {
            repository = hospitalProfilesRepository;
        }

        public List<HospitalProfile> GetAll()
        {
            return repository.GetAll();
        }

        public HospitalProfile GetHospitalProfileByApiKey(string apiKey)
        {
            foreach (HospitalProfile hospitalProfile in repository.GetAll())
            {
                if (apiKey.Equals(hospitalProfile.Key))
                {
                    return hospitalProfile;
                }
            }

            return null;
        }
    }
}
