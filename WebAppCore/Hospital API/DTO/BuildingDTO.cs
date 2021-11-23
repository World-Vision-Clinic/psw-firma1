using Hospital.GraphicalEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class BuildingDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public MapPosition mapPosition { get; set; }
        public List<FloorDTO> floors { get; set; }

        public BuildingDTO(Building building)
        {
            id = building.id;
            name = building.Name;
            info = building.Info;
            mapPosition = new MapPosition();
            floors = new List<FloorDTO>();

        }

        public BuildingDTO()
        {
        }

        internal static Building toBuilding(BuildingDTO buildingDto)
        {
            Building building = new Building();
            building.id = buildingDto.id;
            building.Name = buildingDto.name;
            building.Info = buildingDto.info;
            building.MapPositionId = buildingDto.mapPosition.id;
            building.Area = null;
            return building;
        }
    }
}
