using Hospital.RepositoryInterfaces;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.RoomsAndEquipment.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly HospitalContext dbContext;

        public EquipmentRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            Equipment eq = dbContext.AllEquipment.FirstOrDefault(eq => eq.id == id);
            dbContext.Remove(eq);
            dbContext.SaveChanges();
        }

        public List<Equipment> GetAll()
        {
            List<Equipment> allEq = new List<Equipment>();
            dbContext.AllEquipment.ToList().ForEach(eq => allEq.Add(eq));
            return allEq;
        }

        public Equipment GetByID(int id)
        {
            Equipment eq = dbContext.AllEquipment.FirstOrDefault(eq => eq.id == id);
            return eq;
        }

        public void Save(Equipment eq)
        {
            dbContext.Add(eq);
            dbContext.SaveChanges();
        }

        public void Update(Equipment eq)
        {
            dbContext.Update(eq);
            dbContext.SaveChanges();
        }
    }
}
