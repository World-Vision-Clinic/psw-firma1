using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    class ObjectionsRepository
    {
        IntegrationDbContext dbContext = new IntegrationDbContext();

        public List<Objection> GetAll()
        {
            List<Objection> objections = new List<Objection>();
            dbContext.Objections.ToList().ForEach(objection => objections.Add(objection));
            return objections;
        }

        public void saveEntity(Objection newObjection)
        {
            dbContext.Objections.Add(newObjection);
            dbContext.SaveChanges();
        }
    }
}
