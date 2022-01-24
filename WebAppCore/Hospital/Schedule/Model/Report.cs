using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Report : Entity
    {

        public int AppointmentId { get; set; }
        public string Content { get; set; }

        public Report() { }

        public Report(int appointmentId, string content)
        {
            this.AppointmentId = appointmentId;
            this.Content = content;

        }
    }
}
