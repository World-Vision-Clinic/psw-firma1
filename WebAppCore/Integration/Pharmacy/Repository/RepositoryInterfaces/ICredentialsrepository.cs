using Integration.Repositories.Interfaces;
using Integration.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository.RepositoryInterfaces
{
    public interface ICredentialsRepository : IGenericRepository<Credential>
    {
        Credential GetByPharmacyLocalhost(string pharmacyLocalhost);
    }
}
