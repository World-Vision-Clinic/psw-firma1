using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class ShiftDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public ShiftDTO() { }
        public ShiftDTO(int id, string name, int start, int end)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
        }
    }
}
