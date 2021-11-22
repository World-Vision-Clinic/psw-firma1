using Hospital.GraphicalEditor.Model;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{
    public class Room
    {
        public int id { get; set; }
        private string name;
        public string Name 
        { 
            get { return name; } 
            set { name=value; }       
        }
        private string purpose;
        public string Purpose{
            get{return purpose;}
            set{purpose=value;}
        }
        private int doctorId;
        public int DoctorId{
            get{return doctorId;}
            set{doctorId=value;}
        }
        private int floorId;
        public int FloorId
        {
            get { return floorId; }
            set { floorId = value; }
        }

        //svg render properties
        private int x;
        public int X 
        { 
            get { return x; } 
            set { x=value; }       
        }
        private int y;
        public int Y 
        { 
            get { return y; } 
            set { y=value; }       
        }
        private int height;
        public int Height 
        { 
            get { return height; } 
            set { height=value; }       
        }
        private int width;
        public int Width 
        { 
            get { return width; } 
            set { width=value; }       
        }
        private int doorX;
        public int DoorX 
        { 
            get { return doorX; } 
            set { doorX=value; }       
        }
        private int doorY;
        public int DoorY
        { 
            get { return doorY; } 
            set { doorY=value; }       
        }
        private bool vertical;
        public bool Vertical
        { 
            get { return vertical; } 
            set { vertical=value; }       
        }
        private string css;
        public string Css
        { 
            get { return css; } 
            set { css=value; }       
        }
        private bool doorExist;
        public bool DoorExist
        { 
            get { return doorExist; } 
            set { doorExist=value; }       
        }

        public Room() { }


    }
}
