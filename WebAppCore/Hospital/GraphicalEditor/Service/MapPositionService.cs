using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
    public class MapPositionService
    {
        private MapPositionRepository repository;

        public MapPositionService(MapPositionRepository repos)
        {
            repository = repos;
        }

        public List<MapPosition> getAll()
        {
            return repository.GetAll();
        }

        public MapPosition getById(int id)
        {
            return repository.GetByID(id);

        }
    }
}
