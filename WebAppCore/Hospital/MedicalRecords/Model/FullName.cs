using Hospital.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Model
{
    public class FullName : ValueObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FullName() { }

        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
