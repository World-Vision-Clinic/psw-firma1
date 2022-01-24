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
        public int Id { get; private set; }
        public string Name { get; private set; }
        public EquipmentType Type { get; private set; }

        public int Amount { get; private set; }
        public int RoomId { get; private set; }
        public bool InTransport { get; private set; }
        public DateTime TransportStart { get; private set; }
        public DateTime TransportEnd { get; private set; }

        public Equipment()
        {

        }

        public Equipment(int id, string name, EquipmentType type, int amount, int roomId, bool inTransport, DateTime start, DateTime end)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Amount = amount;
            this.RoomId = roomId;
            this.InTransport = inTransport;
            this.TransportStart = start;
            this.TransportEnd = end;
            Validate();
        }

        public Equipment( string name, EquipmentType type, int amount, int roomId, bool inTransport, DateTime start, DateTime end)
        {
          
            this.Name = name;
            this.Type = type;
            this.Amount = amount;
            this.RoomId = roomId;
            this.InTransport = inTransport;
            this.TransportStart = start;
            this.TransportEnd = end;
        }
            

        public Equipment(int id, string name, EquipmentType type, int amount, int roomId)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Amount = amount;
            this.RoomId = roomId;
            this.InTransport = false;
            Validate();
        }

        public void Validate()
        {
            if (this.Amount < 0)
            {
                throw new Exception();
            }
        }

    }
}
