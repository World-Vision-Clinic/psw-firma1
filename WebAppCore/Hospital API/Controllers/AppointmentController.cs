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

namespace Hospital_API.Controllers
{
    [Route("api/Appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public AppointmentService _appointmentService { get; set; }

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
