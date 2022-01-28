using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class CredentialsService
    {
        ICredentialsRepository repository;

        public CredentialsService(ICredentialsRepository credentialsRepository)
        {
            repository = credentialsRepository; 
        }

        public bool AddNewCredential(Credential newCredential)
        {
            foreach(Credential credential in repository.GetAll())
            {
                if (credential.HospitalLocalhost.Equals(newCredential.HospitalLocalhost))
                {
                    return false;
                }
            }
            repository.Save(newCredential);
            return true;
        }

        public Credential GetByHospitalLocalhost(string hospitalLocalhost)
        {
            return repository.GetByHospitalLocalhost(hospitalLocalhost);
        }

        public Credential GetByHospitalApi(string api)
        {
            return repository.GetByHospitalApi(api);
        }
    }
}
