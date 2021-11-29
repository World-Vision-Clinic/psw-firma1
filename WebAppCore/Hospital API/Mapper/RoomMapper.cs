using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class RoomMapper
    {
        internal static RoomDTO dataToRoomDTO(Room room, EquipmentService equipmentService)
        {
            RoomDTO roomDto = new RoomDTO(room);
            roomDto.equipments = equipmentService.getRoomEquipments(room.Id);
            return roomDto;
        }

        
    }
}
