using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class EquipmentMapper
    {
        internal static EquipmentRoomDTO equipmentToEquipmentRoomDto(Equipment eq, RoomService roomService, FloorService floorService)
        {
            EquipmentRoomDTO eqRoomDto = new EquipmentRoomDTO();
            Room room = roomService.GetById(eq.RoomId);
            Floor floor = floorService.GetById(room.FloorId);
            eqRoomDto.amount = eq.Amount;
            eqRoomDto.roomName = room.Name;
            eqRoomDto.roomId = room.Id;
            eqRoomDto.roomFloor = floor.Level;

            return eqRoomDto;
        }
    }
}
