using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hspital_API.Dto
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public SurveySectionType Section { get; set; }
        public int Answer { get; set; }

        public QuestionDTO(int questionId, string question, SurveySectionType section, int answer)
        {
            this.QuestionId = questionId;
            this.Question = question;
            this.Section = section;
            this.Answer = answer;
        }

        public QuestionDTO() { }
    }
}
