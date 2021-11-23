using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Service;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mappers
{
    public class BuildingMapper
    {
        
        public static BuildingDTO dataToBuildingSimpleDTO(Building building, MapPositionService mapPositionService)
        {
            BuildingDTO buildingDto = new BuildingDTO(building);
            buildingDto.mapPosition = mapPositionService.getById(building.MapPositionId);
           
            return buildingDto;
        }



    }
}
