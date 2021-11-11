using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Service
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
            if (repository.GetAll().Count > 0)
            {
                foreach (Credential credential in repository.GetAll())
                {
                    if (credential.PharmacyLocalhost.Equals(newCredential.PharmacyLocalhost))
                    {
                        return false;
                    }
                }
            }
            repository.Save(newCredential);
            return true;
        }

        public Credential GetByPharmacyLocalhost(string pharmacyLocalhost)
        {
            return repository.GetByPharmacyLocalhost(pharmacyLocalhost);
        }
    }
}
