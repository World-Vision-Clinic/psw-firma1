using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface IObjectionsRepository
    {
        public List<Objection> GetAll();
        public void Save(Objection objection);
        Objection GetById(string id);
    }
}
