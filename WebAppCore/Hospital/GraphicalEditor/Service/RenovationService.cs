using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
    public class RenovationService
    {
        private RenovationRepository repository;

        public RenovationService(RenovationRepository repos)
        {
            repository = repos;
        }

        public Renovation FindRenovationForRoom(int roomId)
        {
            return repository.FindRenovationForRoom(roomId);
        }

        public void Save(Renovation renovation)
        {
            repository.Save(renovation); 
        }

        public void Remove(int renovationId)
        {
            repository.Delete(renovationId);
        }

    }
}
