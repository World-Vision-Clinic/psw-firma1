using Hospital.Models;
using Hospital.Repository;
using Hospital.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyQuestionController : ControllerBase
    {
        private SurveyQuestionService surveyQuestionService;

        public SurveyQuestionController()
        {
            surveyQuestionService = new SurveyQuestionService(new SurveyQuestionRepository(new HospitalContext()));
        }

        [HttpGet("{id}")]
        public ActionResult<SurveyQuestion> GetSurveyQuestion(int id)
        {
            var surveyQuestion = surveyQuestionService.FindById(id);

            if (surveyQuestion == null)
            {
                return NotFound();
            }

            return surveyQuestion;
        }

        [HttpPost]
        public ActionResult<SurveyQuestion> PostSurveyQuestion([FromBody] SurveyQuestion question)
        {
            SurveyQuestion newQuestion = question;

            surveyQuestionService.AddSurveyQuestion(newQuestion);

            return CreatedAtAction("GetSurveyQuestion", new { id = newQuestion.Id }, newQuestion);
        }

    }
}