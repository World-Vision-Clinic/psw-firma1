using Hospital.GraphicalEditor.Model;
using Hospital.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Hospital.SharedModel;


namespace Hospital.GraphicalEditor.Repository
{

    public class BuildingRepository : IBuildingRepository
    {
        private readonly HospitalContext dbContext;

        public BuildingRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            Building building = dbContext.Buildings.FirstOrDefault(building => building.id == id);
            dbContext.Buildings.Remove(building);
            dbContext.SaveChanges();
        }

        public List<Building> GetAll()
        {
            List<Building> buildings = new List<Building>();
            dbContext.Buildings.ToList().ForEach(building => buildings.Add(building));
            return buildings;
        }

        public Building GetByID(int id)
        {
            foreach (Building b in dbContext.Buildings.ToList())
            {
                if (b.id == id)
                {
                    return b;

                }
            }
            
            return null;
        }

        public void Save(Building newBuilding)
        {
            dbContext.Buildings.Add(newBuilding);
            dbContext.SaveChanges();
        }

        public void Update(Building updatedBuilding)
        {
            dbContext.Buildings.Update(updatedBuilding);
            dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return dbContext.Buildings.Any(b => b.id == id);
        }
    }
}
