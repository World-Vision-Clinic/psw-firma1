using Hospital.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class SurveyBreakdownDTO
    {
        public string Question { get; set; }
        public string Section { get; set; }
        public double Average { get; set; }
        public double[] RatingsCount { get; set; }

        public SurveyBreakdownDTO(string question, string section, double average, double[] ratingsCount)
        {
            Question = question;
            Section = section;
            Average = average;
            RatingsCount = ratingsCount;
        }

        public SurveyBreakdownDTO() { }
    }
}
