using Integration.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Partnership.Model
{
    [Owned]
    public class DateRange
    {
        public DateTime FromDate { get; private set; }

        public DateTime ToDate { get; private set; }

        private DateRange()
        { }

        public DateRange(DateTime fromDate, DateTime toDate)
        {
            if(fromDate <= toDate)
            {
                FromDate = fromDate;
                ToDate = toDate;
            }
            else
            {
                throw new DateRangeException("FromDate cannot be after toDate");
            }
        }
    }
}
