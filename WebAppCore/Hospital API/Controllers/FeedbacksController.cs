using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net;
using Hospital_API.Models;
using Hospital.Models;
using Hospital.Service;
using Hospital.Repository;

namespace Hospital_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        //private readonly HospitalContext _context;
        private readonly FeedbackService _feedbackService;
        private readonly PatientService _patientService;

        public FeedbacksController()
        {
            _feedbackService = new FeedbackService(new FeedbackRepository(new Hospital.Models.HospitalContext()));
            _patientService = new PatientService(new PatientRepository(new Hospital.Models.HospitalContext()));
        }

        // GET: api/Feedbacks
        [HttpGet]
        public  ActionResult<IEnumerable<Feedback>> GetFeedbacks()
        {
            //_patientService.AddPatient();
            return _feedbackService.GetAll();
        }

        [HttpGet("published")]
        public ActionResult<IEnumerable<Feedback>> GetFeedbacksPublished()
        {
            return _feedbackService.GetPublished();
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public ActionResult<Feedback> GetFeedback(int id)
        {
            var feedback = _feedbackService.FindById(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutFeedback(int id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }

            _feedbackService.Modify(feedback);

            try
            {
                _feedbackService.SaveSync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Feedbacks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public  ActionResult<Feedback> PostFeedback([FromBody] Feedback feedback)
        {
            Feedback newFeedback = feedback;
            //newFeedback.Id = newFeedback.GetHashCode();

            _feedbackService.AddFeedback(newFeedback);

            return CreatedAtAction("GetFeedback", new { id = newFeedback.Id }, newFeedback);
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public ActionResult<Feedback> DeleteFeedback(int id)
        {
            var feedback = _feedbackService.FindById(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _feedbackService.Delete(feedback);

            return feedback;
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        private bool FeedbackExists(int id)
        {
            return _feedbackService.FeedbackExists(id);
        }
    }
}
