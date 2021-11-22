using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class Building
    {
        public int id { get; set; }
        private string name;
        public string Name 
        { 
            get { return name; } 
            set { name=value; }       
        }
        private string info;
        public string Info 
        { 
            get { return info; } 
            set { info=value; }       
        }
        private Area area;
        public Area Area 
        { 
            get { return area; } 
            set { area=value; }       
        }
        private int mapPositionId;
        public int MapPositionId
        { 
            get { return mapPositionId; } 
            set { mapPositionId=value; }       
        }

    }
}
