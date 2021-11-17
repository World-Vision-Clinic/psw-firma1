using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class ObjectionService
    {
        IObjectionsRepository repository;

        public ObjectionService(IObjectionsRepository objectionRepository)
        {
            repository = objectionRepository;
        }

        public bool AddNewObjection(Objection newObjection)
        {
            foreach (Objection objection in repository.GetAll())
            {
                if (objection.IdEncoded.Equals(newObjection.IdEncoded))
                {
                    return false;
                }
            }
            repository.Save(newObjection);
            return true;
        }

        public Objection GetObjectionById(string id)
        {
            return repository.GetByIdEncoded(id);
        }
    }
}
