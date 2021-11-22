using Hospital.GraphicalEditor.Model;
using Hospital.RepositoryInterfaces;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.GraphicalEditor.Repository
{
    public class FloorRepository : IFloorRepository
    {

        private readonly HospitalContext dbContext;

        public FloorRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            Floor floor = dbContext.Floors.FirstOrDefault(floor => floor.id == id);
            dbContext.Remove(floor);
            dbContext.SaveChanges();
        }

        public List<Floor> GetAll()
        {
            List<Floor> floors = new List<Floor>();
            dbContext.Floors.ToList().ForEach(floor => floors.Add(floor));
            return floors;
        }

        public Floor GetByID(int id)
        {
            Floor floor = dbContext.Floors.FirstOrDefault(floor => floor.id == id);
            return floor;
        }

        public void Save(Floor newFloor)
        {
            dbContext.Add(newFloor);
            dbContext.SaveChanges();
        }

        public void Update(Floor updatedFloor)
        {
            dbContext.Update(updatedFloor);
            dbContext.SaveChanges();
        }

    }
}
