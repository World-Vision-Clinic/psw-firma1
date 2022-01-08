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
    public class Residence : ValueObject
    {
        public string Country { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public Residence() { }

        public Residence(string country, string address, string city)
        {
            Country = country;
            Address = address;
            City = city;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return Address;
            yield return City;
        }
    }
}
