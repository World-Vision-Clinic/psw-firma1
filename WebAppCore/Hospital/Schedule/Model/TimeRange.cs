using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class TimeRange : ValueObject
    {
        public TimeSpan From;
        public TimeSpan To;

        public TimeRange(TimeSpan from, TimeSpan to)
        {
            From = from;
            To = to;
            Validate();
        }

        private void Validate()
        {
            if (From > To)
            {
                throw new Exception("Invalid TimeRange");
            }
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }

        public bool OverlapsWith(TimeRange timeRange) //TODO: Test
        {
            if (From > timeRange.From && From < timeRange.To)
                return true;
            if (To > timeRange.From && To < timeRange.To)
                return true;
            if (timeRange.From > From && timeRange.From < To)
                return true;
            if (timeRange.To > From && timeRange.To < To)
                return true;
            if (timeRange.From == From && timeRange.To == To)
                return true;
            return false;
        }
    }
}
