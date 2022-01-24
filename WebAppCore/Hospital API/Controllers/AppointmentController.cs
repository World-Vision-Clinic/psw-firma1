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
        public PrescriptionMedicineService _prescriptionMedicineService { get; set; }
        public bool test = false;
        public ReportService reportService { get; set; }

        [ActivatorUtilitiesConstructor]
        public AppointmentController()
        {
            HospitalContext context = new HospitalContext();
            IPatientRepository patientRepository = new PatientRepository(context);
            IDoctorRepository doctorRepository = new DoctorRepository(context, patientRepository);
            IAppointmentRepository appointmentRepository = new AppointmentRepository(context);
            IMedicinesRepository medicinesRepository = new MedicinesRepository();
            IReportRepository reportRepository = new ReportRepository(context, appointmentRepository);
            IPrescriptionMedicineRepository prescriptionMedicineRepository = new PrescriptionMedicineRepository(context, appointmentRepository, medicinesRepository);
            _appointmentService = new AppointmentService(appointmentRepository, doctorRepository);
            _patientService = new PatientService(patientRepository, appointmentRepository);
            _prescriptionMedicineService = new PrescriptionMedicineService(prescriptionMedicineRepository);
            reportService = new ReportService(reportRepository);
        }

        public AppointmentController(AppointmentService _appointmentService)
        {
            this._appointmentService = _appointmentService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return _appointmentService.GetAll();
        }

        [Authorize(Roles = "Manager")] //gde se ovo koristi?
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            return _appointmentService.FindById(id);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("prescription/{appointmentId}")]
        public ActionResult<IEnumerable<PrescriptionMedicineDTO>> GetPrescription(int appointmentId)
        {
            Patient patient = getCurrentPatient();
            List<Appointment> appointments = _appointmentService.GetByPatientId(patient.Id);
            bool found = false;
            Appointment requestedAppointment = null;
            foreach (Appointment a in appointments)
            {
                if (a.Id == appointmentId)
                {
                    found = true;
                    requestedAppointment = a;
                    break;
                }
            }
            if (!found)
            {
                return BadRequest("User does not have an appointment with requested id");
            }
            List<PrescriptionMedicine> prescriptions = _prescriptionMedicineService.FindByAppointmentId(appointmentId);
            List<Medicine> medicines = _prescriptionMedicineService.FindMedicinesByAppointmentId(appointmentId);
            List<PrescriptionMedicineDTO> prescriptionDTOs = new List<PrescriptionMedicineDTO>();
            foreach (PrescriptionMedicine pm in prescriptions)
            {
                Medicine medicine = findMedicineInListById(medicines, pm.MedicineId);
                if (medicine == null)
                {
                    return NotFound("Something went wrong on the server side");
                }
                PrescriptionMedicineDTO prescriptionMedicineDTO = new PrescriptionMedicineDTO(pm, medicine);
                prescriptionDTOs.Add(prescriptionMedicineDTO);
            }
            return prescriptionDTOs;
        }


        [Authorize(Roles = "Patient")]
        [HttpGet("report/{appointmentId}")]
        public ActionResult<IEnumerable<Report>> GetReport(int appointmentId)
        {
            Patient patient = getCurrentPatient();
            List<Appointment> appointments = _appointmentService.GetByPatientId(patient.Id);
            bool found = false;
            Appointment requestedAppointment = null;
            foreach (Appointment a in appointments)
            {
                if (a.Id == appointmentId)
                {
                    found = true;
                    requestedAppointment = a;
                    break;
                }
            }
            if (!found)
            {
                return BadRequest("User does not have an appointment with requested id");
            }
            List<Report> reports = reportService.FindByAppointmentId(appointmentId);
        
            return reports;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("patient")]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAppointmentsByPatientId()
        {
            Patient patient = getCurrentPatient();
            List<Appointment> appointments = _appointmentService.GetByPatientId(patient.Id);
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            HospitalContext context = new HospitalContext();
            DoctorRepository doctorRepository = new DoctorRepository(context, new PatientRepository(context));
            SurveyRepository surveyRepository = new SurveyRepository(context);
            foreach (Appointment appointment in appointments)
                appointmentDTOs.Add(new AppointmentDTO(appointment, doctorRepository, surveyRepository));
            return appointmentDTOs;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("doctor/{id}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int id)
        {
            return _appointmentService.GetByDoctorId(id);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("4step/{id}/{dateString}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointments4Step(int id, string dateString)
        {
            DateTime date = Convert.ToDateTime(dateString).Date;
            if (date < DateTime.Now.Date.AddDays(1)) 
            {
                return BadRequest("Date must be tomorrow or onwards");  
            }
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return BadRequest("Hospital doesn't work on weekends");
            }

            List<Appointment> doctorsAppointments = _appointmentService.GetByDoctorIdAndDate(id, date);
            List<Appointment> freeAppointments = _appointmentService.GenerateFreeAppointments(id, date, doctorsAppointments);

            if (freeAppointments.Count == 0)
            {
                return BadRequest("Couldn't find any appointments for Your preferences");
            }

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
            if (patient == null || !patient.Activated)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest }; // Mozda nepotrebno
            if (appointmentToAdd.Date < DateTime.Now)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };

            int appointmentLength = 30;

            appointmentToAdd.PatientForeignKey = patient.Id;
            appointmentToAdd.Length = new TimeSpan(0, appointmentLength, 0);

            if (_appointmentService.GetByDateAndDoctor(appointmentToAdd.Date, appointmentToAdd.Length, appointmentToAdd.DoctorForeignKey) != null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };

            _appointmentService.AddAppointment(appointmentToAdd);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [Authorize(Roles = "Patient")]
        [HttpDelete("{id}")]
        public ActionResult<Appointment> CancelAppointment(int id)
        {
            var appointment = _appointmentService.FindById(id);
            Patient patient = getCurrentPatient();
            if (appointment == null)
                return NotFound();
            if (appointment.PatientForeignKey != patient.Id)
                return Unauthorized();
            if (DateTime.Now > appointment.Date.AddDays(-2) || DateTime.Now > appointment.Date)
                return BadRequest("Cannot cancel this appointment");

            appointment.IsCancelled = true;
            _appointmentService.Modify(appointment);
            return Ok(appointment);
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

        private Medicine findMedicineInListById(List<Medicine> medicines, string id)
        {
            foreach(Medicine m in medicines)
            {
                if (m.ID == id)
                    return m;
            }
            return null;
        }
    }
}
