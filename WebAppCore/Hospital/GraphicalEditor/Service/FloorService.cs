using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
    public class FloorService
    {
        private FloorRepository repository;

        public FloorService(FloorRepository floorRep)
        {
            repository = floorRep;
        }

        public List<Floor> getAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<Floor> getFloorForBuilding(int id)
        {
            return repository.GetFloorsForBuilding(id);
        }

        public Floor GetById(int id)
        {
            return repository.GetByID(id);
        }
    }
}
