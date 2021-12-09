using System;
namespace Hospital.GraphicalEditor.Model
{
    public class DatePeriod
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                DatePeriod p = (DatePeriod)obj;
                return (startDate == p.startDate) && (endDate == p.endDate);
            }
        }
    }
}