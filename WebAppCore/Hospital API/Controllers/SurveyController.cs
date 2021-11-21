using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
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

        // POST: api/Surveys
        [HttpPost]  
        public ActionResult<Survey> PostSurvey([FromBody] Survey survey)
        {
            Survey newSurvey = survey;
            surveyService.AddSurvey(newSurvey);

            return CreatedAtAction("GetSurvey", new { id = newSurvey.IdSurvey }, newSurvey);
        }


    }
}