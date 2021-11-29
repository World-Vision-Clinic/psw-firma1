using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{


    public class RoomDTO
    {

        public int id { get; set; }
        public string name { get; set; }
        public string purpose { get; set; }
        public int doctorId { get; set; }

        public int floorId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int doorX { get; set; }
        public int doorY { get; set; }
        public bool vertical { get; set; }
        public string css { get; set; }
        public bool doorExist { get; set; }
        public List<Equipment> equipments { get; set; }
        public RoomDTO(Room room)
        {
            id = room.Id;
            name = room.Name;
            purpose = room.Purpose;
            doctorId = -1;
            floorId = room.FloorId;
            x = room.X;
            y = room.Y;
            height = room.Height;
            width = room.Width;
            doorX = room.DoorX;
            doorY = room.DoorY;
            vertical = room.Vertical;
            css = room.Css;
            doorExist = room.DoorExist;
            equipments = new List<Equipment>();
        }

        internal Room toRoom()
        {
            Room room = new Room();
            room.Id = id;
            room.Name = name;
            room.Purpose = purpose;
            room.DoctorId = -1;
            room.FloorId =floorId;
            room.X = x;
            room.Y = y;
            room.Height = height;
            room.Width =width;
            room.DoorX = doorX;
            room.DoorY = doorY;
            room.Vertical = vertical;
            room.Css =css;
            room.DoorExist = doorExist;
            return room;
        }
    }
}
