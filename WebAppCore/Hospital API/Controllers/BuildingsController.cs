using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Repository.RepositoryInterfaces;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.GraphicalEditor.Model;
using Hospital_API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Hospital_API.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private RoomService roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private BuildingService buildingService = new BuildingService(new BuildingRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorLabelService floorLabelService = new FloorLabelService(new FloorLabelRepository(new Hospital.SharedModel.HospitalContext()));
        private MapPositionService mapPositionService = new MapPositionService(new MapPositionRepository(new Hospital.SharedModel.HospitalContext()));
        private OutsideDoorService outsideDoorService = new OutsideDoorService(new OutsideDoorRepository(new Hospital.SharedModel.HospitalContext()));

        // GET: api/buildings
        [HttpGet]
        public ActionResult<IEnumerable<BuildingDTO>> GetBuildings()
        {
            List<BuildingDTO> buildings = new List<BuildingDTO>();
            foreach(Building building in buildingService.GetAll())
            {
                //building.MapPositionId = building.id;
                //buildingService.Update(building);
                buildings.Add(BuildingMapper.dataToBuildingSimpleDTO(building, mapPositionService, equipmentService));
            }
            //Console.WriteLine(buildings.ToString());
            return buildings;
        }

        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public ActionResult<BuildingDTO> GetBuilding(int id)
        {
            Building building = buildingService.GetById(id);

            if (building == null)
            {
                return NotFound();
            }
            BuildingDTO dbto = BuildingMapper.dataToBuildingSimpleDTO(building, mapPositionService, equipmentService);


            return dbto;
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutBuilding(int id, BuildingDTO buildingDto)
           
        {
            Building building = BuildingDTO.toBuilding(buildingDto);
            if (id != building.id)
            {
                return BadRequest();
            }

            try
            {
                buildingService.Update(building);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool BuildingExists(int id)
        {
            return buildingService.BuildingExists(id);
        }

    }
}

