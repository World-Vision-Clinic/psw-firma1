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
            //(int id, string name, EquipmentType type, int amount, int roomId, bool inTransport, DateTime start, DateTime end)
            Equipment retEquipment = new Equipment(equipment.Id, equipment.Name, equipment.Type, Amount, TargetRoomId, true, UnixTimeStampToDateTime(startDate), UnixTimeStampToDateTime(endDate));
            
            return retEquipment;
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
