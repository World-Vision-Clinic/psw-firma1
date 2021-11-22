using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class FloorMapper
    {
        internal static FloorDTO floorToFloorDTO(Floor floor, RoomService roomService)
        {
            FloorDTO floorDto = new FloorDTO(floor);
            floorDto.rooms = roomService.getRoomsForFloor(floor.id);
            return floorDto;
        }
    }
}
