using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class Area
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
        private int width;
        public int Width 
        { 
            get { return width; } 
            set { width=value; }       
        }
        private int height;
        public int Height 
        { 
            get { return height; } 
            set { height=value; }       
        }

    }
}
