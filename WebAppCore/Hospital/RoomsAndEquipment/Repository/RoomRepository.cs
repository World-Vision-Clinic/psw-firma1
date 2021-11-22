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
            dbContext.Remove(room);
            dbContext.SaveChanges();
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
            dbContext.Add(room);
            dbContext.SaveChanges();
        }

        public void Update(Room room)
        {
            dbContext.Update(room);
            dbContext.SaveChanges();
        }
    }
}
