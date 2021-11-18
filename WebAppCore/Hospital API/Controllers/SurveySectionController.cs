using Hospital.Models;
using Hospital_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hospital_API
{/*
    public class SurveySectionController : ControllerBase
    {
        private readonly HospitalContext _context;

        public SurveySectionController(HospitalContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SurveySection>> PostSurvey([FromBody] SurveySection section)
        {
            SurveySection newSection = section;

            _context.SurveySections.Add(newSection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = newSection.Id }, newSection);
        }

    }
    */
}