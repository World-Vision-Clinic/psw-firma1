using Hospital.Models;
using Hospital_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hospital_API
{
    public class SurveyController : ControllerBase
    {
        private readonly HospitalContext _context;

        public SurveyController(HospitalContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Survey>> PostSurvey([FromBody] Survey survey)
        {
            Survey newSurvey = survey;

            _context.Surveys.Add(newSurvey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = newSurvey.Id }, newSurvey);
        }

    }
}