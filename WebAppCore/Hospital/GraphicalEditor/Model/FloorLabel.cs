using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class FloorLabel
    {
        public int id {get; set;}
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
        private string text;
        public string Text {
            get{return text;}
            set{text=value;}
        }
        private int floorId;
        public int FloorId
        {
            get { return floorId; }
            set { floorId = value; }
        }

        public FloorLabel() {
            
        }
    }
}
