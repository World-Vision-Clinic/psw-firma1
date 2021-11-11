using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Repository
{
    public class ObjectionsRepository : IObjectionsRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();
        public List<Objection> GetAll()
        {
            List<Objection> objections = new List<Objection>();
            dbContext.Objections.ToList().ForEach(objection => objections.Add(objection));
            return objections;
        }

        public void Save(Objection objection)
        {
            dbContext.Objections.Add(objection);
            dbContext.SaveChanges();
        }

        public Objection GetByIdEncoded(string idEncoded)
        {
            List<Objection> objections = new List<Objection>();
            dbContext.Objections.ToList().ForEach(objection => objections.Add(objection));
            foreach (Objection objection in objections)
            {
                if (objection.IdEncoded.Equals(idEncoded))
                {
                    return objection;
                }
            }
            return null;
        }
    }
}
