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
            Floor floor = dbContext.Floors.FirstOrDefault(floor => floor.Id == id);
            dbContext.Floors.Remove(floor);
            dbContext.SaveChanges();
        }

        internal IEnumerable<Floor> GetFloorsForBuilding(int id)
        {
            List<Floor> floors = new List<Floor>();
            dbContext.Floors.ToList().ForEach(floor => {
                if (floor.BuildingId == id)
                    floors.Add(floor);
            });
            return floors;
        }

        public List<Floor> GetAll()
        {
            List<Floor> floors = new List<Floor>();
            dbContext.Floors.ToList().ForEach(floor => floors.Add(floor));
            return floors;
        }

        public Floor GetByID(int id)
        {
            Floor floor = dbContext.Floors.FirstOrDefault(floor => floor.Id == id);
            return floor;
        }

        public void Save(Floor newFloor)
        {
            dbContext.Floors.Add(newFloor);
            dbContext.SaveChanges();
        }

        public void Update(Floor updatedFloor)
        {
            dbContext.Floors.Update(updatedFloor);
            dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return false;
        }
    }
}
