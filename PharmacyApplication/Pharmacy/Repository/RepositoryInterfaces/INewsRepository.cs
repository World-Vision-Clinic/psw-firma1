using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
   public interface INewsRepository
    {
        public void Save(News news);
    }
}
