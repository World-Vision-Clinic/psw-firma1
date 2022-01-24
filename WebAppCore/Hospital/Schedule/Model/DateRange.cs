using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class DateRange : ValueObject
    {
        public DateTime From;
        public DateTime To;

        public DateRange(DateTime from, DateTime to)
        {
            From = from;
            To = to;
            Validate();
        }

        private void Validate()
        {
            if (From > To)
            {
                throw new Exception("Invalid DateRange");
            }
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }

        public bool OverlapsWith(DateRange dateRange)
        {
            if (From <= dateRange.From && To > dateRange.To)
                return true;
            if (From >= dateRange.From && From < dateRange.To)
                return true;
            if (dateRange.From <= From && dateRange.To > From)
                return true;
            if (dateRange.From >= From && dateRange.From < To)
                return true;
            return false;
        }
    }
}
