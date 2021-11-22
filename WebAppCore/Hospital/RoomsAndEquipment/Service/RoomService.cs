using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Service
{
    public class RoomService
    {
        private RoomRepository repository;
        public RoomService(RoomRepository repos)
        {
            repository = repos;
        }
        public List<Room> getAll()
        {
            return repository.GetAll();
        }

    }
}
