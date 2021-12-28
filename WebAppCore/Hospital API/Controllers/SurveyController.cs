using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API.DTO;
using Hospital_API.Mapper;
using Hspital_API.Dto;
using Hspital_API.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hospital_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        public SurveyService surveyService { get; set; }
        public AppointmentService _appointmentService { get; set; }
        public PatientService _patientService { get; set; }

        public SurveyController()
        {
            surveyService = new SurveyService(new SurveyRepository(new HospitalContext()));
            IAppointmentRepository appointmentRepository = new AppointmentRepository(new HospitalContext());
            IPatientRepository patientRepository = new PatientRepository(new HospitalContext());
            _patientService = new PatientService(patientRepository, appointmentRepository);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("{id}")]
        public ActionResult<Survey> GetSurvey(int id)
        {
            var survey = surveyService.FindById(id);

            if (survey == null)
            {
                return NotFound();
            }
            return survey;
        }

        [Authorize(Roles = "Patient, Manager")]
        [HttpGet]
        public ActionResult<IEnumerable<QuestionDTO>> GetQuestions()
        {
            List<QuestionDTO> dtoList = new List<QuestionDTO>();
            foreach (SurveyQuestion question in surveyService.GetAllQuestions())
            {
                dtoList.Add(QuestionMapper.QuestionToQuestionDTO(question));
            }
            return Ok(dtoList);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("answered_questions_breakdown")]
        public ActionResult<IEnumerable<SurveyBreakdownDTO>> GetAnsweredQuestionsBreakdown()
        {
            return SurveyBreakdownMapper.AllSurveysToSurveyBreakdownDTO(surveyService.GetAllAnsweredQuestions());
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("{id}")]
        public ActionResult<Survey> PostSurveyQuestions([FromBody] List<QuestionDTO> questions, int id)
        {
            Survey newSurvey = new Survey(System.DateTime.Now);

            Patient patient = getCurrentPatient();

            List<Appointment> appointments = _appointmentService.GetByPatientId(patient.Id);

            bool found = false;
            foreach (Appointment a in appointments)
            {
                if (a.Id == id)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return Unauthorized();
            }

            int IdSurvey = surveyService.AddSurvey(newSurvey);

            foreach(QuestionDTO dtos in questions)
            {
                surveyService.AddAnswer(QuestionMapper.QuestionDTOToAnswer(dtos, IdSurvey));
            }
            return Ok(newSurvey);
        }

        private Patient getCurrentPatient()
        {
            string username = User.FindFirst("username")?.Value;
            Patient patient = _patientService.FindByUserName(username);
            return patient;
        }
    }
}