using Hospital.RepositoryInterfaces;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.RoomsAndEquipment.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HospitalContext dbContext;

        public RoomRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            Room room = dbContext.Rooms.FirstOrDefault(room => room.id == id);
            dbContext.Rooms.Remove(room);
            dbContext.SaveChanges();
        }

        internal List<Room> GetRoomsForFloor(int floorId)
        {
            List<Room> rooms = new List<Room>();
            dbContext.Rooms.ToList().ForEach(room =>
                {
                    if (room.FloorId == floorId)
                        rooms.Add(room);

                }
            );
            return rooms;
        }


        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();
            dbContext.Rooms.ToList().ForEach(room => rooms.Add(room));
            return rooms;
        }

        public Room GetByID(int id)
        {
            Room room = dbContext.Rooms.FirstOrDefault(room => room.id == id);
            return room;
        }

        public void Save(Room room)
        {
            dbContext.Rooms.Add(room);
            dbContext.SaveChanges();
        }

        internal List<int> GetRoomsForFloors(List<int> floorIds)
        {
            List<int> roomIds = new List<int>();
            dbContext.Rooms.ToList().ForEach(room => {
                if (floorIds.Contains(room.FloorId))
                {
                    roomIds.Add(room.id);
                }
            });
            return roomIds;
        }

        public void Update(Room room)
        {
            dbContext.Rooms.Update(room);
            dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return dbContext.Rooms.Any(r => r.id == id);
        }
    }
}
