using Integration.Pharmacy.Model;
using Integration.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository.RepositoryInterfaces
{
  public interface INewsRepository : IGenericRepository<News>
    {
        public void Update();
    }
}
