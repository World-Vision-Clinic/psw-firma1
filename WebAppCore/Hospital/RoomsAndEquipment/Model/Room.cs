using Hospital.GraphicalEditor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{
    public class Room
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public int DoctorId { get; set; }

        public int FloorId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int DoorX { get; set; }
        public int DoorY { get; set; }
        public bool Vertical { get; set; }
        public string Css { get; set; }
        public bool DoorExist { get; set; }

        public Room() { }


    }
}
