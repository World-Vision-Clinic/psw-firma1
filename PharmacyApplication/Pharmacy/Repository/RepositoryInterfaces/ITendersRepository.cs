using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface ITendersRepository
    {
        public List<Tender> GetAll();
        public void Save(Tender tender);
        public Tender GetById(string tenderId);
    }
}
