using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class EquipmentRoomDTO
    {
        public string roomName { get; set; }
        public string roomFloor { get; set; }
        public int amount { get; set; }
        public int roomId { get; set; }
    }
}
