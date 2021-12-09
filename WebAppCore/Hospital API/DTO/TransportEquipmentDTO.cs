using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class TransportEquipmentDTO
    {
        public int TargetEqupmentId { get; set; }
        public int TargetRoomId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int Amount { get; set; }
        internal Equipment getEquipment(EquipmentService equipmentService)
        {
            Equipment equipment = equipmentService.getById(TargetEqupmentId);

            equipment.InTransport = true;
            equipment.Amount = Amount;
            equipment.TransportStart = startDate;
            equipment.TransportEnd = endDate;
            equipment.RoomId = TargetRoomId;
            return equipment;
        }

        
    }
}
