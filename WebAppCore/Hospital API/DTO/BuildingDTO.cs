using Hospital.GraphicalEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class BuildingDTO
    {
        public int id;

        public string Name { get; set; }
        private string info;

        public BuildingDTO(Building building)
        {
            id = building.id;
            Name = building.Name;
            Info = building.Info;
            MapPosition = new MapPosition();
            Area = building.Area;

        }

        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        public Area Area { get; set; }
        public MapPosition MapPosition { get; set; }
    }
}
