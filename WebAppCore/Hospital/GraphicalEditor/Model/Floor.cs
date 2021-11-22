using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class Floor
    {
        public int id { get; set; }
        private string level;
        public string Level{
            get{return level;}
            set{level=value;}
        }
        private string info;
        public string Info{
            get{return info;}
            set{info=value;}
        }
        private int buildingId;
        public int BuildingId
        {
            get { return buildingId; }
            set { buildingId = value; }
        }

    }
}
