using Hospital.GraphicalEditor.Model;
using Hospital.RepositoryInterfaces;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.GraphicalEditor.Repository.RepositoryInterfaces
{
   public class FloorLabelRepository : IFloorLabelRepository
    {

        private readonly HospitalContext dbContext;

        public FloorLabelRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            FloorLabel floorL = dbContext.FloorLabels.FirstOrDefault(floorL => floorL.id == id);
            dbContext.FloorLabels.Remove(floorL);
            dbContext.SaveChanges();
        }

        public List<FloorLabel> GetAll()
        {
            List<FloorLabel> floorLabels = new List<FloorLabel>();
            dbContext.FloorLabels.ToList().ForEach(floorL => floorLabels.Add(floorL));
            return floorLabels;
        }

        public FloorLabel GetByID(int id)
        {
            FloorLabel floorL = dbContext.FloorLabels.FirstOrDefault(floorL => floorL.id == id);
            return floorL;
        }

        public void Save(FloorLabel newLabel)
        {
            dbContext.FloorLabels.Add(newLabel);
            dbContext.SaveChanges();
        }

        public void Update(FloorLabel updatedLabel)
        {
            dbContext.FloorLabels.Update(updatedLabel);
            dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return false;
        }
    }
}
