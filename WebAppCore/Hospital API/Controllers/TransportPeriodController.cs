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
        private BuildingService buildingService = new BuildingService(new BuildingRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorLabelService floorLabelService = new FloorLabelService(new FloorLabelRepository(new Hospital.SharedModel.HospitalContext()));
        private MapPositionService mapPositionService = new MapPositionService(new MapPositionRepository(new Hospital.SharedModel.HospitalContext()));
        private OutsideDoorService outsideDoorService = new OutsideDoorService(new OutsideDoorRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentTransportationService equipmentTransportationService = new EquipmentTransportationService(new EquipmentTransportationRepository(new Hospital.SharedModel.HospitalContext()));
        public bool test = false;


        [HttpGet]
        public ActionResult<TransportationPeriodDTO> GetTransportationSuggestion([FromQuery] int buildingId, [FromQuery] double transportDurationInHours, [FromQuery] long startDateTimeStamp, [FromQuery] long endDateTimeStamp)
        {


            DateTime startDate = DateTimeOffset.FromUnixTimeSeconds(startDateTimeStamp/1000).LocalDateTime;
            DateTime endDate = DateTimeOffset.FromUnixTimeSeconds(endDateTimeStamp / 1000).LocalDateTime;
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 8, 0, 0);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 8, 0, 0);
            List<int> roomIds = roomService.getRoomIdsForBuilding(buildingId, floorService);
            TransportationPeriodDTO tpDto = new TransportationPeriodDTO();
            DateTime workingStartDate = startDate;
            DateTime workingEndDate = workingStartDate.AddHours(transportDurationInHours);
            List<Equipment> equipmentsInTransport = equipmentService.getAllInTransport(roomIds);
            equipmentsInTransport = equipmentsInTransport.OrderBy(x => x.TransportStart).ToList<Equipment>();
            foreach (Equipment equipment in equipmentsInTransport)
            {
                if(workingStartDate >= equipment.TransportStart && equipment.TransportEnd <= workingEndDate )
                {
                    workingStartDate = equipment.TransportEnd.AddMinutes(5);
                    workingEndDate = workingStartDate.AddHours(transportDurationInHours);
                    if (workingEndDate > endDate)
                        return NotFound();
                }
            }
            tpDto.startDate = workingStartDate;
            tpDto.endDate = workingEndDate;

            return tpDto;
        }


        [HttpPost]
        public IActionResult SetTransportation(TransportEquipmentDTO transportEquipmentDTO)
        {
            try
            {

                Equipment targetEquipment = equipmentService.getById(transportEquipmentDTO.TargetEqupmentId);
                Equipment newTargetEq = new Equipment(targetEquipment.Id,targetEquipment.Name,targetEquipment.Type, targetEquipment.Amount-transportEquipmentDTO.Amount,targetEquipment.RoomId);
                equipmentService.Update(newTargetEq);

                Equipment equipmentInTransport = new Equipment(-1,targetEquipment.Name, targetEquipment.Type, transportEquipmentDTO.Amount, transportEquipmentDTO.TargetRoomId, true, transportEquipmentDTO.startDate, transportEquipmentDTO.endDate);              
                equipmentService.Create(equipmentInTransport);
            }
            catch
            {
                throw;
            }
            return Ok();
        }

        [HttpPost("quickTransport")]
        public IActionResult QuickTransport(TransportEquipmentDTO dto)
        {
            Equipment targetEquipment = equipmentService.getById(dto.TargetEqupmentId);
            Equipment newEq = new Equipment(targetEquipment.Name, targetEquipment.Type, 15, dto.TargetRoomId, true, dto.startDate,dto.endDate);
            Room r1 = roomService.GetById(dto.TargetRoomId);
            equipmentService.Create(newEq);
            equipmentTransportationService.MakeTransportFromStorage(newEq.Id, newEq, null, r1, dto.startDate, dto.endDate);
            return Ok();
        }
    }
}
