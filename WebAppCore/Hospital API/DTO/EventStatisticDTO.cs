using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public enum EventStatisticType
    {
        TABLE,
        GRAPH
    }
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
        public EventStatisticType Type { get; set; }
        public List<EventStatisticDataPair> Data { get; set; }

        public EventStatisticDTO(string name)
        {
            this.Name = name;
            this.Data = new List<EventStatisticDataPair>();
            this.Type = EventStatisticType.GRAPH;
        }
        public EventStatisticDTO(string name, EventStatisticType type)
        {
            this.Name = name;
            this.Data = new List<EventStatisticDataPair>();
            this.Type = type;
        }

        public EventStatisticDTO(string name, List<EventStatisticDataPair> data)
        {
            this.Name = name;
            this.Data = data;
            this.Type = EventStatisticType.GRAPH;
        }
        public EventStatisticDTO(string name, EventStatisticType type, List<EventStatisticDataPair> data)
        {
            this.Name = name;
            this.Data = data;
            this.Type = type;
        }
    }
}
