using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Repository.RepositoryInterfaces;
using Hospital.GraphicalEditor.Service;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital_API.DTO;
using Hospital_API.Mapper;
using Hspital_API.Dto;
using Hspital_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Controllers
{
    [Route("api/equipment")]
    [ApiController]
    public class EquimpentController : ControllerBase
    {
        private RoomService roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private BuildingService buildingService = new BuildingService(new BuildingRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorLabelService floorLabelService = new FloorLabelService(new FloorLabelRepository(new Hospital.SharedModel.HospitalContext()));
        private MapPositionService mapPositionService = new MapPositionService(new MapPositionRepository(new Hospital.SharedModel.HospitalContext()));
        private OutsideDoorService outsideDoorService = new OutsideDoorService(new OutsideDoorRepository(new Hospital.SharedModel.HospitalContext()));
       

        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetEquipment([FromQuery] int buildingId, [FromQuery] string searchText)
        {

            List<int> roomIds = roomService.getRoomIdsForBuilding(buildingId, floorService);
            
            List<Equipment> equipment = equipmentService.getUniqueInBuilding(roomIds);
           
            return equipment;
        }


        [HttpGet("byRooms")]
        public ActionResult<IEnumerable<EquipmentRoomDTO>> GetRoomEquipment([FromQuery] string equipmentName, [FromQuery] int buildingId)
        {
            List<int> roomIds = roomService.getRoomIdsForBuilding(buildingId, floorService);
            List<EquipmentRoomDTO> equipmentList = new List<EquipmentRoomDTO>();
            foreach(Equipment eq in equipmentService.getByNameInBuilding(roomIds, equipmentName))
            {
                equipmentList.Add(EquipmentMapper.equipmentToEquipmentRoomDto(eq, roomService, floorService));
            }


            return equipmentList;
        }

        [HttpPut("/transport/{id}")]
        public IActionResult cancelTransport(int id, TransportDTO dto)

        {
            Equipment eq = equipmentService.getById(dto.id);
            eq.InTransport = false;

            if (id != eq.Id)
            {
                return BadRequest();
            }

            try
            {
                equipmentService.Update(eq);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
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

        private bool EquipmentExists(int id)
        {
            return equipmentService.EquipmentExists(id);
        }


    }
}
