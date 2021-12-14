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
    public class RenovationController : ControllerBase
    {
        private RoomService roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()), new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private RenovationService renovationService = new RenovationService(new RenovationRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorLabelService floorLabelService = new FloorLabelService(new FloorLabelRepository(new Hospital.SharedModel.HospitalContext()));
        private MapPositionService mapPositionService = new MapPositionService(new MapPositionRepository(new Hospital.SharedModel.HospitalContext()));
        private OutsideDoorService outsideDoorService = new OutsideDoorService(new OutsideDoorRepository(new Hospital.SharedModel.HospitalContext()));


        // GET: api/Renovation/room/5
        [HttpGet("room/{id}")]
        public ActionResult<Renovation> GetRenovationForRoom(int id)
        {
            Renovation renovation = renovationService.FindRenovationForRoom(id);
            if (renovation == null)
            {
                return NotFound();
            }

            return renovation;
        }

        // GET: api/Rooms/5
        [HttpDelete("{id}")]
        public IActionResult RemoveRenovation(int id)
        {
            try
            {
                renovationService.Remove(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }


        // GET: api/Rooms/5
        [HttpPost()]
        public IActionResult CreateRenovation(RenovationDTO renovationDTO)
        {
            try
            {
                Renovation renovation = renovationDTO.toRenovation();
                renovationService.Save(renovation);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }



    }
}

