using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface ICredentialsRepository
    {
        public List<Credential> GetAll();
        public void Save(Credential credential);
        public Credential GetByHospitalLocalhost(string hospitalLocalhost);

        public Credential GetByHospitalName(string hospitalName);

        public Credential GetByHospitalApi(string api);
    }
}
