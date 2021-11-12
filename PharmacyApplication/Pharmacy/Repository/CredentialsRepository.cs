using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Repository
{
    public class CredentialsRepository : ICredentialsRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();
        public List<Credential> GetAll()
        {
            List<Credential> credentials = new List<Credential>();
            dbContext.Credentials.ToList().ForEach(credential => credentials.Add(credential));
            return credentials;
        }

        public Credential GetByHospitalLocalhost(string hospitalLocalhost)
        {
            List<Credential> credentials = new List<Credential>();
            dbContext.Credentials.ToList().ForEach(credential => credentials.Add(credential));
            foreach (Credential credential in credentials)
            {
                if (credential.HospitalLocalhost.Equals(hospitalLocalhost))
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
