using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Integration.Pharmacy.Model
{
    public class PharmacyProfile
    {
        public string Name { get; set; }
        public string Key { get; set; }
        [Key]
        public string Localhost { get; set; }
        public ProtocolType Protocol { set; get; }
        public string Address { set; get; }
        public string City { get; set; }

        public PharmacyProfile(string name, string key, string localhost, ProtocolType protocol, string address, string city)
        {
            Name = name;
            Key = key;
            Localhost = localhost;
            Protocol = protocol;
            Address = address;
            City = city;
        }

        public PharmacyProfile() { }
    }
}
