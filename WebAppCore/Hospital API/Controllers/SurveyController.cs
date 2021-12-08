using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API.DTO;
using Hospital_API.Mapper;
using Hspital_API.Dto;
using Hspital_API.Mapper;
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

        public SurveyController()
        {
            surveyService = new SurveyService(new SurveyRepository(new HospitalContext()));
        }

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

        [HttpGet("answered_questions_breakdown")]
        public ActionResult<IEnumerable<SurveyBreakdownDTO>> GetAnsweredQuestionsBreakdown()
        {
            return SurveyBreakdownMapper.AllSurveysToSurveyBreakdownDTO(surveyService.GetAllAnsweredQuestions());
        }

        [HttpPost]
        public ActionResult<Survey> PostSurveyQuestions([FromBody] List<QuestionDTO> questions, int id)
        {
            Survey newSurvey = new Survey();
            newSurvey.CreationDate = System.DateTime.Now;
            newSurvey.IdAppointment = 1;
           // var appointment = _appointmentService.FindById(id);
            //newSurvey.IdAppointment = appointment.Id;

            foreach (QuestionDTO dtos in questions)
            {
                if(dtos.Answer <1 || dtos.Answer >5)
                {
                    return BadRequest();
                }
            }

            int IdSurvey = surveyService.AddSurvey(newSurvey);

            foreach(QuestionDTO dtos in questions)
            {
                surveyService.AddAnswer(QuestionMapper.QuestionDTOToAnswer(dtos, IdSurvey));
            }
            return Ok(newSurvey);
        }
    }
}