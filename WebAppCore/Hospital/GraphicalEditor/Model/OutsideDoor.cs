using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class OutsideDoor
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
        private bool isVertical;
        public bool IsVertical{
            get{return isVertical;}
            set{isVertical=value;}
        }

        private int mapPositionId;
        public int MapPositionId
        {
            get { return mapPositionId; }
            set { mapPositionId = value; }
        }

    }
}
