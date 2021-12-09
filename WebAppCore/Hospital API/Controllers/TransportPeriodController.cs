using Hospital.GraphicalEditor.Model;
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
using Hspital_API.Dto;
using Hspital_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Controllers
{
    [Route("api/transportPeriod")]
    [ApiController]
    public class TransportPeriodController : ControllerBase
    {
        private RoomService roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));


        [HttpGet]
        public ActionResult<DatePeriod> GetTransportationSuggestion([FromQuery] int buildingId, [FromQuery] double transportDurationInHours, [FromQuery] long startDateTimeStamp, [FromQuery] long endDateTimeStamp)
        {
            DateTime startDate = DateTimeOffset.FromUnixTimeSeconds(startDateTimeStamp / 1000).LocalDateTime;
            DateTime endDate = DateTimeOffset.FromUnixTimeSeconds(endDateTimeStamp / 1000).LocalDateTime;
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 8, 0, 0);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 8, 0, 0);

            DatePeriod datePeriod = equipmentService.SuggestTransportationPeriod(startDate, endDate, buildingId, floorService, roomService, equipmentService, transportDurationInHours);
            if (datePeriod == null) return NotFound();

            return datePeriod;
        }


        [HttpPost]
        public IActionResult SetTransportation(TransportEquipmentDTO transportEquipmentDTO)
        {
            try
            {

                equipmentService.reduceAmount(transportEquipmentDTO.TargetEqupmentId, transportEquipmentDTO.Amount);
                Equipment equipmentInTransport = transportEquipmentDTO.getEquipment(equipmentService);
                equipmentService.Create(equipmentInTransport);
            }
            catch
            {
                throw;
            }
            return Ok();
        }


    }
}