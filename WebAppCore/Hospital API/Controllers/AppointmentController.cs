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
using Hospital.SharedModel;
using Hospital.MedicalRecords.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;

namespace Hospital_API.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public PatientService _patientService { get; set; }
        public AppointmentService _appointmentService { get; set; }
        public bool test = false;

        [ActivatorUtilitiesConstructor]
        public AppointmentController()
        {
            HospitalContext context = new HospitalContext();
            IPatientRepository patientRepository = new PatientRepository(context);
            IDoctorRepository doctorRepository = new DoctorRepository(context, patientRepository);
            _patientService = new PatientService(patientRepository);
            _appointmentService = new AppointmentService(doctorRepository, patientRepository);
        }

        public AppointmentController(AppointmentService _appointmentService)
        {
            this._appointmentService = _appointmentService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return _patientService.GetAllAppointments();
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("patient")]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAppointmentsByPatientId()
        {
            Patient patient = getCurrentPatient();
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            HospitalContext context = new HospitalContext();
            DoctorRepository doctorRepository = new DoctorRepository(context, new PatientRepository(context));
            SurveyRepository surveyRepository = new SurveyRepository(context);
            if(patient != null && patient.Appointments != null)
                foreach (Appointment appointment in patient.Appointments)
                    appointmentDTOs.Add(new AppointmentDTO(appointment, doctorRepository, surveyRepository));
            return appointmentDTOs;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("doctor/{id}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int id)
        {
            return _patientService.GetAppointmentsByDoctorId(id);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("4step/{id}/{dateString}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointments4Step(int id, string dateString)
        {
            DateTime date = Convert.ToDateTime(dateString).Date;
            if (date < DateTime.Now.Date.AddDays(1))
                return BadRequest("Date must be tomorrow or onwards");
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return BadRequest("Hospital doesn't work on weekends");

            List<Appointment> doctorsAppointments = _patientService.GetAppointmentsByDoctorIdAndDate(id, date);
            List<Appointment> freeAppointments = _appointmentService.GenerateFreeAppointments(id, date, doctorsAppointments);

            if (freeAppointments.Count == 0)
                return BadRequest("Couldn't find any appointments matching your preferences");

            return Ok(freeAppointments);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("recommendation_doctor")]
        public ActionResult<IEnumerable<Appointment>> GetRecommendedAppointments([FromBody] AppointmentRecommendationRequestDTO appointmentRecommendationRequest)
        {
            DateRange dateRange = new DateRange(appointmentRecommendationRequest.LowerDateRange, appointmentRecommendationRequest.UpperDateRange);
            TimeSpan lowerTime = TimeSpan.Parse(appointmentRecommendationRequest.LowerTimeRange);
            TimeSpan upperTime = TimeSpan.Parse(appointmentRecommendationRequest.UpperTimeRange);
            TimeRange timeRange = new TimeRange(lowerTime, upperTime);
            AppointmentSearchPriority priorityType;

            if (String.Equals(appointmentRecommendationRequest.PriorityType, "DOCTOR_PRIORITY")) //TODO: Automatska konverzija?
                priorityType = AppointmentSearchPriority.DOCTOR_PRIORITY;
            else
                priorityType = AppointmentSearchPriority.DATE_TIME_PRIORITY;
            
            return _appointmentService.GetAvailableByDateRangeAndDoctor(dateRange, timeRange, appointmentRecommendationRequest.DoctorId, priorityType);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("add_appointment")]
        public HttpResponseMessage AddAppointment([FromBody] Appointment appointmentToAdd)
        {
            Patient patient = getCurrentPatient();
            if (patient == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };

            bool result = patient.AddAppointment(appointmentToAdd);
            if(result)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
        }

        [Authorize(Roles = "Patient")]
        [HttpDelete("{id}")]
        public ActionResult<Appointment> CancelAppointment(int id)
        {
            Patient patient = getCurrentPatient();
            if (patient == null)
                return Unauthorized();
            if (patient.CancelAppointment(id))
                return null;
            return BadRequest("Cannot cancel this appointment");
        }

        private Patient getCurrentPatient()
        {
            if (test)
            {
                Patient patient = _patientService.FindByUserName("Marko123");
                return patient;
            }
            else
            {
                string username = User.FindFirst("username")?.Value;
                Patient patient = _patientService.FindByUserName(username);
                return patient;
            }

        }
    }
}
