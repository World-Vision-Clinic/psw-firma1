using Hospital.GraphicalEditor.Model;
using Hospital.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
    public class BuildingService
    {
        private IBuildingRepository repository;

        public BuildingService(IBuildingRepository buildingRepository)
        {
            repository = buildingRepository;
        }

        public bool AddNewBuilding(Building newBuilding)
        {
            if (repository.GetAll().Count > 0)
            {
                foreach (Building building in repository.GetAll())
                {
                    if (building.Equals(newBuilding))
                    {
                        return false;
                    }
                }
            }
            repository.Save(newBuilding);
            return true;
        }

        public List<Building> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(Building building)
        {
            repository.Update(building);
        }

        public bool BuildingExists(int id)
        {
            return repository.Exists(id);
        }
    }
}
