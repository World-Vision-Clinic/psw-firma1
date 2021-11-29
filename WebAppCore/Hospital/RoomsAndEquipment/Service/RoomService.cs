using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
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

        public List<Room> getRoomsForFloor(int id)
        {
            return repository.GetRoomsForFloor(id);
        }

        public List<Room> getRoomsForFloorAndBuilding(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public Room GetById(int id)
        {
            return repository.GetByID(id);
        }

        public bool RoomExists(int id)
        {
            return repository.Exists(id);
        }

        public void Update(Room room)
        {
            repository.Update(room);
        }

        public List<int> getRoomIdsForBuilding(int buildingId, FloorService floorService)
        {
            List<int> floorIds = new List<int>();
            foreach(Floor floor in floorService.getFloorForBuilding(buildingId))
            {
                floorIds.Add(floor.Id);
            }
            return repository.GetRoomsForFloors(floorIds);

        }
    }
}
