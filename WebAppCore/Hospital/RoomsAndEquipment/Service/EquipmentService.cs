using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Service
{
    public class EquipmentService
    {
        private EquipmentRepository repository;

        public EquipmentService(EquipmentRepository repos)
        {
            repository = repos;
        }

        public List<Equipment> getAll()
        {
            return repository.GetAll();
        }

        public List<Equipment> getRoomEquipments(int roomId)
        {
            return repository.GetRoomEquipemnts(roomId);
        }
    }
}
