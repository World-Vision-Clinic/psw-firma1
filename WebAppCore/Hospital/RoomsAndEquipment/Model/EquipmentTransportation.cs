using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{
    public class EquipmentTransportation
    {
        public int Id { get; set; }
        public Equipment Equipment { get; private set; }
        public Room RoomFrom { get; private set; }
        public Room RoomTo { get; private set; }
        [NotMapped]
        public TransportPeriod Period {get; private set;}
        public List<EquipmentTransportationEvent> Events { get; set; }
        public int Version { get; private set; }

        public EquipmentTransportation() { }
        public EquipmentTransportation(int id, Equipment equipment, Room roomFrom, Room roomTo, TransportPeriod period, List<EquipmentTransportationEvent> events, int version)
        {
            this.Id = id;
            this.Equipment = equipment;
            this.RoomFrom = roomFrom;
            this.RoomTo = roomTo;
            this.Period = period;
            this.Events = events;
            this.Version = version;
        }
        public EquipmentTransportation(Equipment equipment, Room roomFrom, Room roomTo, DateTime d1, DateTime d2)
        {
            this.Equipment = equipment;
            this.RoomFrom = roomFrom;
            this.RoomTo = roomTo;
            this.Period = new TransportPeriod(d1, d2);
        }

        public void addNewEvent(EquipmentTransportationEvent transEvent){
            if (this.Events == null)
            {
                this.Events = new List<EquipmentTransportationEvent>();
                this.Events.Add(transEvent);
            } else this.Events.Add(transEvent);
            this.Version++;
        }

        public void ordinaryTransport()
        {
            this.addNewEvent(new EquipmentTransportationEvent(this.Id, "Equipment transported from " + this.RoomFrom.Name + " to " + this.RoomTo.Name));
        }

        public void fromStorageTransport()
        {
            this.addNewEvent(new EquipmentTransportationEvent(this.Id, "Equipment transported from storage to " + this.RoomTo.Name));
        }

        public void emptyRoomEquipment()
        {
            this.addNewEvent(new EquipmentTransportationEvent(this.Id, "Equipment removed from room."));
        }

        public void roomRenovation()
        {
            this.addNewEvent(new EquipmentTransportationEvent(this.Id, "Equipment changed room because of renovation."));
        }

    }
}
