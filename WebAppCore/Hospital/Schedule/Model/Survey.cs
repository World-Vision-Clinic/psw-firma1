using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Survey
    {
        private SurveySection section;
        private int id;

        public SurveySection Section
        {
            get { return section; }
            set { section = value; }
            
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
