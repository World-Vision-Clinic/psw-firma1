using Hospital.Models;
using Hospital.Repository;
using Hospital.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveySectionController : ControllerBase
    {
        private SurveySectionService surveySectionService;

        public SurveySectionController()
        {
            surveySectionService = new SurveySectionService(new SurveySectionRepository(new HospitalContext()));
        }

        [HttpGet("{id}")]
        public ActionResult<SurveySection> GetSurveySection(int id)
        {
            var surveySection = surveySectionService.FindById(id);

            if (surveySection == null)
            {
                return NotFound();
            }

            return surveySection;
        }

        [HttpPost]
        public ActionResult<SurveySection> PostSurveySection([FromBody] SurveySection section)
        {
            SurveySection newSection = section;

            surveySectionService.AddSurveySection(newSection);

            return CreatedAtAction("GetSurveySection", new { id = newSection.Id }, newSection);
        }

    }
    
}