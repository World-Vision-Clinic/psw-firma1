using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Schedule.Service;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Model;
using System.Net.Http;
using System.Net;
using Hospital_API.DTO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Hospital_API.Controllers
{
    [Route("api/Appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public AppointmentService _appointmentService { get; set; }

        [ActivatorUtilitiesConstructor]
        public AppointmentController()
        {
            _appointmentService = new AppointmentService(new AppointmentRepository(new Hospital.SharedModel.HospitalContext()));
        }

        public AppointmentController(AppointmentService _appointmentService)
        {
            this._appointmentService = _appointmentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return _appointmentService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            return _appointmentService.FindById(id);
        }

        [HttpGet("patient/{id}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointmentsByPatientId(int id)
        {
            return _appointmentService.GetByPatientId(id);
        }

        [HttpGet("doctor/{id}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int id)
        {
            return _appointmentService.GetByDoctorId(id);
        }

        [HttpGet("4step/{id}/{dateString}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointments4Step(int id, string dateString)
        {
            List<Appointment> freeAppointments = new List<Appointment>();
            DateTime date = Convert.ToDateTime(dateString);
            if (date < DateTime.Now) 
            {
                return BadRequest(freeAppointments);  
            }

            List<Appointment> doctorsAppointments = _appointmentService.GetByDoctorIdAndDate(id, date);

            //freeAppointments = _appointmentService.GenerateFreeAppointments(id,date,doctorsAppointments);
            Appointment appointment = new Appointment()
            {
                Id = 1,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = DateTime.Now,
                Time = TimeSpan.Zero
            };
            freeAppointments.Add(appointment);
            return Ok(freeAppointments);
        }

        [HttpPost("recommendation_doctor")]
        public ActionResult<IEnumerable<Appointment>> GetRecommendedAppointmentsByDoctorPriority([FromBody] AppointmentRecommendationRequestDTO appointmentRecommendationRequest)
        {
            return _appointmentService.GetAvailableByDateRangeAndDoctor(appointmentRecommendationRequest.LowerDateRange, appointmentRecommendationRequest.UpperDateRange, TimeSpan.Parse(appointmentRecommendationRequest.LowerTimeRange), TimeSpan.Parse(appointmentRecommendationRequest.UpperTimeRange), appointmentRecommendationRequest.DoctorId, AppointmentSearchPriority.DOCTOR_PRIORITY);
        }

        [HttpPost("add_appointment")]
        public HttpResponseMessage AddAppointment([FromBody] Appointment appointmentToAdd)
        {
            if (appointmentToAdd.Date < DateTime.Now)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };

            if (_appointmentService.GetByDateAndDoctor(appointmentToAdd.Date, appointmentToAdd.Time, appointmentToAdd.DoctorForeignKey) != null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };

            _appointmentService.AddAppointment(appointmentToAdd);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}
