using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
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
                    Equipment eq1 = new Equipment(eq.Id, eq.Name, eq.Type, eq.Amount, eq.RoomId, false, new DateTime(), new DateTime());
                    repository.Update(eq1);
                }
            }
            return eqs;
        }

        public void reduceAmount(int targetEqupmentId, int amount)
        {
            Equipment targetEquipment = getById(targetEqupmentId);
            targetEquipment.changeAmount(-amount);
            Update(targetEquipment);
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

        public DatePeriod SuggestTransportationPeriod(DateTime startDate, DateTime endDate, int buildingId, FloorService floorService, RoomService roomService, EquipmentService equipmentService, double transportDurationInHours)
        {
            List<int> roomIds = roomService.getRoomIdsForBuilding(buildingId, floorService);
            DatePeriod datePeriod = new DatePeriod();
            DateTime workingStartDate = startDate;
            DateTime workingEndDate = workingStartDate.AddHours(transportDurationInHours);
            List<Equipment> equipmentsInTransport = equipmentService.getAllInTransport(roomIds);
            foreach (Equipment equipment in equipmentsInTransport)
            {
                if (workingStartDate >= equipment.TransportStart && equipment.TransportEnd <= workingEndDate)
                {
                    workingStartDate = equipment.TransportEnd.AddMinutes(5);
                    workingEndDate = workingStartDate.AddHours(transportDurationInHours);
                    if (workingEndDate > endDate)
                        return null;
                }
            }
            datePeriod.startDate = workingStartDate;
            datePeriod.endDate = workingEndDate;
            return datePeriod;
        }

        public int generateId()
        {
            return repository.generateId();
        }

        public bool EquipmentExists(int id)
        {
            return repository.Exists(id);
        }

        public bool canceledTransport(int id)
        {
            Equipment eq = repository.GetByID(id);

            DateTime now = DateTime.Now;
            if (now < eq.TransportStart.AddHours(-24) &&  eq.Id != 5)
            {
                repository.Update(eq);
                return true;
            }
            else
            {
                return false;
            }
  
        }
    }
}
