using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class SurveyAnswerBreakdown
    {
        public string Question { get; set; }
        public double Average { get; set; }
        public double[] RatingsCount { get; set; } 
    }
}
