using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    public class CredentialsRepository : ICredentialsRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();
        public List<Credential> GetAll()
        {
            List<Credential> credentials = new List<Credential>();
            dbContext.Credentials.ToList().ForEach(credential => credentials.Add(credential));
            return credentials;
        }

        public Credential GetByPharmacyLocalhost(string pharmacyLocalhost)
        {
            List<Credential> credentials = new List<Credential>();
            dbContext.Credentials.ToList().ForEach(credential => credentials.Add(credential));
            foreach (Credential credential in credentials)
            {
                if (credential.PharmacyLocalhost.Equals(pharmacyLocalhost))
                {
                    return credential;
                }
            }
            return null;
        }

        public void Save(Credential credential)
        {
            dbContext.Credentials.Add(credential);
            dbContext.SaveChanges();
        }
    }
}
