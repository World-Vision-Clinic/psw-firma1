using Hospital.Models;
using Hospital_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hospital_API
{/*
    public class SurveyQuestionController : ControllerBase
    {
        private readonly HospitalContext _context;

        public SurveyQuestionController(HospitalContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SurveyQuestion>> PostSurvey([FromBody] SurveyQuestion question)
        {
            SurveyQuestion newQuestion = question;

            _context.SurveyQuestions.Add(newQuestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = newQuestion.Id }, newQuestion);
        }

    }*/
}