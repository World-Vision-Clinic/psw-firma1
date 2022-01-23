using Hospital.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Model
{
    [Keyless]
    [NotMapped]
    public class FullName : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public FullName() { }

        public FullName(string firstName, string lastName)
        {
            this.FirstName = Char.ToUpper(firstName[0]) + "";
            if (firstName.Length > 1)
                this.FirstName += firstName.Substring(1);

            this.LastName = Char.ToUpper(lastName[0]) + "";
            if (lastName.Length > 1)
                this.LastName += lastName.Substring(1);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
