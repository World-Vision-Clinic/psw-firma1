using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class FloorDTO
    {
        public int id { get; set; }
        public string level { get; set; }
        public string info { get; set; }
        public List<RoomDTO> rooms { get; set; }
        private Floor floor;

        public FloorDTO(Floor floor)
        {
            id = floor.id;
            level = floor.Level;
            info = floor.Info;
            rooms = new List<RoomDTO>();
        }
    }
}
