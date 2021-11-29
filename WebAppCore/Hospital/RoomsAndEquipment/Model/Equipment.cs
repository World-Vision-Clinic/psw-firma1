using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{

   public enum EquipmentType
    {
        STATIC,
        DYNAMIC
    }

    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }

        public int Amount { get; set; }
        public int RoomId { get; set; }
        public bool InTransport { get; set; }
        public DateTime TransportStart { get; set; }
        public DateTime TransportEnd { get; set; }

        public Equipment()
        {

        }

    }
}
