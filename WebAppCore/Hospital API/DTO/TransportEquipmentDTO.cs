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
        public long startDate { get; set; }
        public long endDate { get; set; }
        public int Amount { get; set; }

        internal Equipment getEquipment(EquipmentService equipmentService)
        {
            Equipment equipment = equipmentService.getById(TargetEqupmentId);

            equipment.InTransport = true;
            equipment.Amount = Amount;
            equipment.TransportStart = UnixTimeStampToDateTime(startDate);
            equipment.TransportEnd = UnixTimeStampToDateTime(endDate);
            equipment.RoomId = TargetRoomId;
            return equipment;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            if (unixTimeStamp > 1040242800000)
                unixTimeStamp = unixTimeStamp / 1000;
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
