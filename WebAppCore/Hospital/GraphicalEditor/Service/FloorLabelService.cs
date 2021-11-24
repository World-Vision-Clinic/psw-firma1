using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
    public class FloorLabelService
    {
        private FloorLabelRepository repository;

        public FloorLabelService(FloorLabelRepository repos)
        {
            repository = repos;
        }

        public List<FloorLabel> getAll()
        {
            return repository.GetAll();
        }
    }
}
