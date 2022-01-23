using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{
    public class TransportPeriod
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public TransportPeriod() { }
        public TransportPeriod(DateTime s, DateTime e)
        {
            this.Start = s;
            this.End = e;
            this.Validate();
        }

        public void Validate()
        {
            if (this.End < this.Start)
            {
                throw new Exception();
            }
        }
    }
}
