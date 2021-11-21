using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hspital_API.Dto
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public SurveySectionType Section { get; set; }
        public int Answer { get; set; }
        public int IdSurvey { get; set; }

        public QuestionDTO(int id, string question, SurveySectionType section, int answer, int idSurvey)
        {
            Id = id;
            Question = question;
            Section = section;
            Answer = answer;
            IdSurvey = idSurvey;
        }

        public QuestionDTO() { }
    }
}
