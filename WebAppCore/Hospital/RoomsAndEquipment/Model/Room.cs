using Hospital.GraphicalEditor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{
    public class Room
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Purpose { get; private set; }
        public int DoctorId { get; private set; }

        public int FloorId { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int DoorX { get; private set; }
        public int DoorY { get; private set; }
        public bool Vertical { get; private set; }
        public string Css { get; private set; }
        public bool DoorExist { get; private set; }

        public Room() { }

        public Room(int id, string name, string purpose, int docId, int florId, int x, int y, int h, int w, int dx, int dy, bool vertical, string css, bool dexist)
        {
            this.Id = id;
            this.Name = name;
            this.Purpose = purpose;
            this.DoctorId = docId;
            this.FloorId = florId;
            this.X = x;
            this.Y = y;
            this.Height = h;
            this.Width = w;
            this.DoorX = dx;
            this.DoorY = dy;
            this.Vertical = vertical;
            this.Css = css;
            this.DoorExist = dexist;
            Validate();            
        }

        public void Validate()
        {
            if(this.X < 0 || this.Y < 0 || this.Height < 0 || this.Width < 0 )
            {
                throw new Exception();
            }

        }

    }
}
