using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
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
        internal static FloorDTO floorToFloorDTO(Floor floor, RoomService roomService, EquipmentService equipmentService)
        {
            FloorDTO floorDto = new FloorDTO(floor);
            foreach(Room room in roomService.getRoomsForFloor(floor.id))
            {
                floorDto.rooms.Add(RoomMapper.dataToRoomDTO(room, equipmentService));
            }
            
            return floorDto;
        }
    }
}
