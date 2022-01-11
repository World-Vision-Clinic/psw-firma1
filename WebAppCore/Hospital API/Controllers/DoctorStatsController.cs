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
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Hospital.MedicalRecords.Repository;
using Hospital_API.Mapper;
using Hospital.ShiftsAndVacations.Service;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.Schedule.Service;
using Hospital.Schedule.Repository;
using System.Net.Http;
using System.Net;

namespace Hospital_API.Controllers
{
    [Route("api/doctorStats")]
    [ApiController]
    public class DoctorStatsController : ControllerBase
    {
        private RoomService roomService = new RoomService(new RoomRepository(new Hospital.SharedModel.HospitalContext()));
        private EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new Hospital.SharedModel.HospitalContext()));
        private BuildingService buildingService = new BuildingService(new BuildingRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorService floorService = new FloorService(new FloorRepository(new Hospital.SharedModel.HospitalContext()));
        private FloorLabelService floorLabelService = new FloorLabelService(new FloorLabelRepository(new Hospital.SharedModel.HospitalContext()));
        private MapPositionService mapPositionService = new MapPositionService(new MapPositionRepository(new Hospital.SharedModel.HospitalContext()));
        private OutsideDoorService outsideDoorService = new OutsideDoorService(new OutsideDoorRepository(new Hospital.SharedModel.HospitalContext()));
        private DoctorService doctorService = new DoctorService(new DoctorRepository(new Hospital.SharedModel.HospitalContext()));
        private VacationService vacationService = new VacationService(new VacationRepository(new Hospital.SharedModel.HospitalContext()));
        private OnCallShiftService onCallShiftService = new OnCallShiftService(new OnCallShiftRepository(new Hospital.SharedModel.HospitalContext()));
        private AppointmentService appointmentService = new AppointmentService(new AppointmentRepository(new Hospital.SharedModel.HospitalContext()), new DoctorRepository(new Hospital.SharedModel.HospitalContext()));
        public BuildingService buildingTestService;


       

        [HttpPost("")]
        public ActionResult<List<DoctorStatDTO>> GetDoctorStats([FromBody] DoctorStatRequestDTO request)
        //public ActionResult<List<DoctorStatDTO>> GetDoctorStats([FromBody]  DoctorStatRequestDTO request)
        {

            DateTime startDate = request.StartDate;
            DateTime endDate = request.EndDate;
            List <DoctorStatDTO> stats = new List<DoctorStatDTO>();
            foreach(Doctor doctor in doctorService.GetAll())
            {
                //building.MapPositionId = building.id;
                //buildingService.Update(building);
                //DoctorStatDTO d =;
                stats.Add(DoctorStatsMapper.getDoctorStats(doctor, startDate, endDate, vacationService, onCallShiftService, appointmentService));
            }
            //Console.WriteLine(buildings.ToString());
            return stats;
        }

       

    }
}

