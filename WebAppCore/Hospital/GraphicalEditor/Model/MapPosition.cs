using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class MapPosition
    {
        public int id {get; set;}
        private int x { get; set; }
         public int X
        { 
            get { return x; } 
            set { x=value; }       
        }
        private int y { get; set; }
         public int Y 
        { 
            get { return y; } 
            set { y=value; }       
        }
        private int height { get; set; }
        public int Height 
        { 
            get { return height; } 
            set { height=value; }       
        }
        private int width { get; set; }
        public int Width 
        { 
            get { return width; } 
            set { width=value; }       
        }      
    }
}
