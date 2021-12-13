using Hospital.GraphicalEditor.Model;
using Hospital.RepositoryInterfaces;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.GraphicalEditor.Repository
{
    public class RenovationRepository : IRenovationRepository
    {
        private readonly HospitalContext dbContext;

        public RenovationRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            Renovation renovation = dbContext.Renovations.FirstOrDefault(door => door.id == id);
            dbContext.Renovations.Remove(renovation);
            dbContext.SaveChanges();
        }

        public List<Renovation> GetAll()
        {
            List<Renovation> renovations = new List<Renovation>();
            dbContext.Renovations.ToList().ForEach(r => renovations.Add(r));
            return renovations;
        }

        public Renovation GetByID(int id)
        {
            Renovation renovation = dbContext.Renovations.FirstOrDefault(r => r.id == id);
            return renovation;
        }

        public void Save(Renovation renovation)
        {
            dbContext.Renovations.Add(renovation);
            dbContext.SaveChanges();
        }

        public void Update(Renovation renovation)
        {
            dbContext.Renovations.Update(renovation);
            dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return false;
        }
    }
}
