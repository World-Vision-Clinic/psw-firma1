using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    public class Floor
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Info { get; set; }
        public int BuildingId { get; set; }

    }
}
