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
using Hospital.SharedModel;

namespace Hospital_API.Controllers
{
    [Route("api/Rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private RoomService roomService ;
        private EquipmentService equipmentService;
        

        public RoomsController(HospitalContext hospitalContext)
        {
            roomService = new RoomService(new RoomRepository(hospitalContext));
            equipmentService = new EquipmentService(new EquipmentRepository(hospitalContext));
            
        }

        public RoomsController()
        {
             roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()));
             equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
             
        }

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


        private bool RoomExists(int id)
        {
            return roomService.RoomExists(id);
        }

    }
}

