using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hspital_API.Dto
{
    public class QuestionDTO
    {
        public string Question { get; set; }
        public SurveySectionType Section { get; set; }
        public int Answer { get; set; }

        public QuestionDTO(string question, SurveySectionType section, int answer)
        {
            Question = question;
            Section = section;
            Answer = answer;
        }

        public QuestionDTO() { }
    }
}
