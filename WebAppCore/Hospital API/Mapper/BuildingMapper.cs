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
        public static BuildingDTO dataToBuildingMapDTO(Building building, MapPositionService mapPositionService, FloorService floorService, RoomService roomService)
        {
            BuildingDTO buildingDto = new BuildingDTO(building);
            buildingDto.mapPosition = mapPositionService.getById(building.MapPositionId);
            buildingDto.floors = new List<FloorDTO>();
            foreach (Floor floor in floorService.getFloorForBuilding(building.id))
            {
                FloorDTO floorDto = new FloorDTO(floor);
                floorDto.rooms = roomService.getRoomsForFloorAndBuilding(building.id, floor.id);
                buildingDto.floors.Add(floorDto);
            }
            return buildingDto;
        }

        public static BuildingDTO dataToBuildingSimpleDTO(Building building, MapPositionService mapPositionService)
        {
            BuildingDTO buildingDto = new BuildingDTO(building);
            buildingDto.mapPosition = mapPositionService.getById(building.MapPositionId);
           
            return buildingDto;
        }



    }
}
