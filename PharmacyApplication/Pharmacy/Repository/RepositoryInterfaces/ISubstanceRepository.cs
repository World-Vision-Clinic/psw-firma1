using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface ISubstanceRepository
    {
        public List<Substance> GetAll();
    }
}