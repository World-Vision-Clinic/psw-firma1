using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Repository.RepositoryInterfaces;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel;
using Hospital_API.DTO;
using Hospital_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private RoomService roomService ;
        private EquipmentService equipmentService ;
        private BuildingService buildingService;
        private FloorService floorService;
        

        public FloorsController(HospitalContext hospitalContext)
        {
            roomService = new RoomService(new RoomRepository(hospitalContext));
            equipmentService = new EquipmentService(new EquipmentRepository(hospitalContext));
            buildingService = new BuildingService(new BuildingRepository(hospitalContext));
            floorService = new FloorService(new FloorRepository(hospitalContext));
            

        }

        public FloorsController()
        {
            roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()));
            equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
            buildingService = new BuildingService(new BuildingRepository(new Hospital.SharedModel.HospitalContext()));
            floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
            
        }

        // GET FLOORS FOR BUILDING
        // GET: api/Floors?buildingId=3
        [HttpGet]
        public ActionResult<IEnumerable<FloorDTO>> GetFloorsForBuilding([FromQuery] int buildingId)
        {

            List<FloorDTO> floors = new List<FloorDTO>();
            foreach (Floor floor in floorService.getFloorForBuilding(buildingId))
            {
               
                floors.Add(FloorMapper.floorToFloorDTO(floor, roomService, equipmentService));
            }
            return floors.OrderBy(x => x.id).ToArray();
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        public ActionResult<FloorDTO> GetBuilding(int id)
        {
            Floor floor = floorService.GetById(id);

            if (floor == null)
            {
                return NotFound();
            }
            FloorDTO bdto = new FloorDTO(floor);

            return bdto;
        }

        


        private bool BuildingExists(int id)
        {
            return buildingService.BuildingExists(id);
        }

    }
}
