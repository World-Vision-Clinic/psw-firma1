using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class RoomMergeDTO
    {
        public Room room1 { get; set; }
        public Room room2 { get; set; }
        public String name { get; set; }
        public String purpose { get; set; }

        public RoomMergeDTO()
        {

        }
        public RoomMergeDTO(Room r1, Room r2, String nam, String purp)
        {
            this.room1 = r1;
            this.room2 = r2;
            this.name = nam;
            this.purpose = purp;
        }
    }
}
