using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{
    public class EquipmentTransportationEvent
    {
        public int Id { get; set; }
        public int AggregateId { get; set; }
        public string ReasonForTransportation { get; set; }
        public DateTime TimeOfTransport { get; set; }

        public EquipmentTransportationEvent()
        {

        }
        public EquipmentTransportationEvent(int id, int agregateId, string reason)
        {
            this.Id = id;
            this.ReasonForTransportation = reason;
            this.TimeOfTransport = DateTime.Now;
        }

        public EquipmentTransportationEvent(int agregateId,string reason)
        {
            this.AggregateId = agregateId;
            this.ReasonForTransportation = reason;
            this.TimeOfTransport = DateTime.Now;
        }
    }
}
