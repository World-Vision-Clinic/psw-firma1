using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hospital_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private SurveyService surveyService;

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

        [HttpPost]
        public ActionResult<Survey> PostSurvey([FromBody] Survey survey)
        {
            Survey newSurvey = survey;

            surveyService.AddSurvey(newSurvey);

            return CreatedAtAction("GetSurvey", new { id = newSurvey.Id }, newSurvey);
        }


    }
}