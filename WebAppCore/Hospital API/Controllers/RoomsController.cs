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
using Hospital.RoomsAndEquipment.Model;
using Hospital_API.Mapper;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Cors;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private RoomService roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()), new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private BuildingService buildingService = new BuildingService(new BuildingRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorLabelService floorLabelService = new FloorLabelService(new FloorLabelRepository(new Hospital.SharedModel.HospitalContext()));
        private MapPositionService mapPositionService = new MapPositionService(new MapPositionRepository(new Hospital.SharedModel.HospitalContext()));
        private OutsideDoorService outsideDoorService = new OutsideDoorService(new OutsideDoorRepository(new Hospital.SharedModel.HospitalContext()));

       
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRooms()
        {
            
            return roomService.getAll();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public ActionResult<RoomDTO> GetRoom(int id)
        {
            Room room = roomService.GetById(id);

            if (room == null)
            {
                return NotFound();
            }
            RoomDTO rdto = RoomMapper.dataToRoomDTO(room, equipmentService);

            return rdto;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutRooms(int id, RoomDTO roomDto)

        {
            Room room = roomDto.toRoom();
            if (id != room.Id)
            {
                return BadRequest();
            }

            try
            {
                roomService.Update(room);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        [HttpPost("merge")]
        public IActionResult Merge(RoomMergeDTO dto)
        {
            try
            {
                int a = roomService.mergeRooms(dto.room1, dto.room2, dto.name, dto.purpose);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost("split")]
        public IActionResult Split(RoomSplitDTO dto)
        {
            try
            {
                roomService.splitRoom(dto.room, dto.name1, dto.purpose1, dto.name2, dto.purpose2);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }


        private bool RoomExists(int id)
        {
            return roomService.RoomExists(id);
        }

    }
}

