using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class EventStatisticDataPair
    {
        public string Label { get; set; }
        public float Value { get; set; }

        public EventStatisticDataPair(string label, float value)
        {
            this.Label = label;
            this.Value = value;
        }
    }
    public class EventStatisticDTO
    {
        public string Name { get; set; }
        public List<EventStatisticDataPair> Data { get; set; }

        public EventStatisticDTO(string name)
        {
            this.Name = name;
            this.Data = new List<EventStatisticDataPair>();
        }

        public EventStatisticDTO(string name, List<EventStatisticDataPair> data)
        {
            this.Name = name;
            this.Data = data;
        }
    }
}
