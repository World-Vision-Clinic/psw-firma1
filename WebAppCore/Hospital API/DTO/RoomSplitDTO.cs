using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class RoomSplitDTO
    {
        public Room room { get; set; }
        public String name1 { get; set; }
        public String name2 { get; set; }
        public String purpose1 { get; set; }
        public String purpose2 { get; set; }

        public RoomSplitDTO() { }

        public RoomSplitDTO(Room r, String n1, String n2, String p1, String p2)
        {
            this.room = r;
            this.name1 = n1;
            this.name2 = n2;
            this.purpose1 = p1;
            this.purpose2 = p2;
        }
    }
}
