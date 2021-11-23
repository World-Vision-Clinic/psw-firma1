using Hospital.GraphicalEditor.Model;
using Hospital.RepositoryInterfaces;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.GraphicalEditor.Repository
{
    public class OutsideDoorRepository : IOutsideDoorRepository
    {
        private readonly HospitalContext dbContext;

        public OutsideDoorRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            OutsideDoor door = dbContext.OutsideDoors.FirstOrDefault(door => door.id == id);
            dbContext.OutsideDoors.Remove(door);
            dbContext.SaveChanges();
        }

        public List<OutsideDoor> GetAll()
        {
            List<OutsideDoor> doors = new List<OutsideDoor>();
            dbContext.OutsideDoors.ToList().ForEach(door => doors.Add(door));
            return doors;
        }

        public OutsideDoor GetByID(int id)
        {
            OutsideDoor door = dbContext.OutsideDoors.FirstOrDefault(door => door.id == id);
            return door;
        }

        public void Save(OutsideDoor door)
        {
            dbContext.OutsideDoors.Add(door);
            dbContext.SaveChanges();
        }

        public void Update(OutsideDoor door)
        {
            dbContext.OutsideDoors.Update(door);
            dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return false;
        }
    }
}
