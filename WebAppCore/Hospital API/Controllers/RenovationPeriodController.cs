using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Repository.RepositoryInterfaces;
using Hospital.GraphicalEditor.Service;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
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
    [Route("api/renovationPeriod")]
    [ApiController]
    public class RenovationPeriodController : ControllerBase
    {
        private RoomService roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private BuildingService buildingService = new BuildingService(new BuildingRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorLabelService floorLabelService = new FloorLabelService(new FloorLabelRepository(new Hospital.SharedModel.HospitalContext()));
        private MapPositionService mapPositionService = new MapPositionService(new MapPositionRepository(new Hospital.SharedModel.HospitalContext()));
        private OutsideDoorService outsideDoorService = new OutsideDoorService(new OutsideDoorRepository(new Hospital.SharedModel.HospitalContext()));
        private RenovationService renovationService = new RenovationService(new RenovationRepository(new Hospital.SharedModel.HospitalContext()));
        private AppointmentService appointmentService = new AppointmentService(new AppointmentRepository(new Hospital.SharedModel.HospitalContext()), new DoctorRepository(new Hospital.SharedModel.HospitalContext()));

        [HttpPost("/suggestion")]
        public ActionResult<TransportationPeriodDTO> GetRenovationSuggestion(RenovationRequestDTO renovationRequestDTO)
        {
            try {
                return (TransportationPeriodDTO)renovationService.getSuggestions(roomService, appointmentService, equipmentService, renovationRequestDTO.Room1Id, renovationRequestDTO.Room2Id, renovationRequestDTO.StartPeriodTimestamp, renovationRequestDTO.EndPeriodTimestamp, renovationRequestDTO.DurationInDays);
            }
            catch
            {
                return NotFound();
            }

        }


        

        private DateTime convertTimestampToDatetime(long timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }


    }
}
