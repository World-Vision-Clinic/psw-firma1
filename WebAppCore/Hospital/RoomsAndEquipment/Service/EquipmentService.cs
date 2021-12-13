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
            List<Equipment> eqs = repository.GetRoomEquipemnts(roomId);
            foreach(Equipment eq in eqs)
            {
                if(eq.InTransport && eq.TransportEnd < DateTime.Now)
                {
                    eq.InTransport = false;
                    eq.TransportStart = new DateTime();
                    eq.TransportEnd = new DateTime();
                    repository.Update(eq);
                }
            }
            return eqs;
        }

        public List<Equipment> getAllInTransport(List<int> roomIds)
        {
            return repository.GetAllInTransport(roomIds);
        }

        public Equipment getById(int targetEqupmentId)
        {
            return repository.GetByID(targetEqupmentId);
        }

        public void Update(Equipment targetEquipment)
        {
            repository.Update(targetEquipment);
        }

        public void Create(Equipment equipmentInTransport)
        {
            repository.Save(equipmentInTransport);
        }

        public List<Equipment> getUniqueInBuilding(List<int> roomIds)
        {
            return repository.getUniqueInBuilding(roomIds);
        }

        public IEnumerable<Equipment> getByNameInBuilding(List<int> roomIds, string equipmentName)
        {
            return repository.GetByNameInBuilding(roomIds, equipmentName);
        }

        public bool EquipmentExists(int id)
        {
            return repository.Exists(id);
        }

    }
}
