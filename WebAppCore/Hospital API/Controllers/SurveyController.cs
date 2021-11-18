using Hospital.Models;
using Hospital_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hospital_API
{/*
    public class SurveyController : ControllerBase
    {
        private readonly HospitalContext _context;
        private SurveyService surveyService;

        public SurveyController(HospitalContext context)
        {
            _context = context;
            surveyService = new SurveyService(context);
        }

        [HttpPost]
        public async Task<ActionResult<Survey>> PostSurvey([FromBody] Survey survey)
        {
            Survey newSurvey = surveyService.AddSurvey(survey);

            return CreatedAtAction("GetFeedback", new { id = newSurvey.Id }, newSurvey);
        }

    }*/
}