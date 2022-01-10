using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    [Owned]
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }

        private Address()
        { }

        public Address(string street, string city)
        {
            if (street.Length > 0 && city.Length > 0)
            {
                Street = street;
                City = city;
            }
            else
            {
                throw new Exception("Invalid address info");
            }
        }
    }
}
