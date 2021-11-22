using Hospital.GraphicalEditor.Model;
using Hospital.RepositoryInterfaces;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.GraphicalEditor.Repository
{
    public class MapPositionRepository : IMapPositionRepository
    {
        private readonly HospitalContext dbContext;

        public MapPositionRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            MapPosition position = dbContext.MapPositions.FirstOrDefault(pos => pos.id == id);
            dbContext.Remove(position);
            dbContext.SaveChanges();
        }

        public List<MapPosition> GetAll()
        {
            List<MapPosition> positions = new List<MapPosition>();
            dbContext.MapPositions.ToList().ForEach(pos => positions.Add(pos));
            return positions;
        }

        public MapPosition GetByID(int id)
        {
            MapPosition position = dbContext.MapPositions.FirstOrDefault(pos => pos.id == id);
            return position;
        }

        public void Save(MapPosition position)
        {
            dbContext.Add(position);
            dbContext.SaveChanges();
        }

        public void Update(MapPosition position)
        {
            dbContext.Update(position);
            dbContext.SaveChanges();
        }
    }
}
