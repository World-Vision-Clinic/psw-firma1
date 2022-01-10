using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Exceptions
{
    public class DateRangeException : Exception
    {
        public DateRangeException() : base() { }
        public DateRangeException(string message) : base(message) { }
        public DateRangeException(string message, Exception e) : base(message, e) { }
    }
}
