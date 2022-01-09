using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Model
{
    public class Shift
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Start { get; private set; }
        public int End { get; private set; }

        public Shift() { }

        public Shift(int id, string name, int start, int end)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
        }
        
    }
}
