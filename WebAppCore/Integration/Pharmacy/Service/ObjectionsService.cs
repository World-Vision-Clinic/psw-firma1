using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Service
{
   public class ObjectionsService
    {
        ObjectionsRepository objectionsRepository = new ObjectionsRepository();

        public List<Objection> GetAll()
        {
            return objectionsRepository.GetAll();
        }
    }
}
