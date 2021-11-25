using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
    public class OutsideDoorService
    {
        private OutsideDoorRepository repository;

        public OutsideDoorService(OutsideDoorRepository repos)
        {
            repository = repos;
        }

        public List<OutsideDoor> getAll()
        {
            return repository.GetAll();
        }
    }
}
