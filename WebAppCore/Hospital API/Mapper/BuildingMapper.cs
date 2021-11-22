using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mappers
{
    public class BuildingMapper
    {
        public static BuildingDTO dataToBuildingMapDTO(Building building, MapPositionService mapPositionService)
        {
            BuildingDTO buildingDto = new BuildingDTO(building);
            buildingDto.MapPosition = mapPositionService.getById(building.MapPositionId);
            return buildingDto;
        }
    }
}
