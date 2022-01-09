using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Integration.Pharmacy.Model
{
    public class PharmacyProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ConnectionInfo ConnectionInfo { get; set; }
        
        public Address Address { get; set; }

        public string Note { get; set; }

        public PharmacyProfile(string name, string key, string localhost, ProtocolType protocol, string street, string city, string note)
        {
            Name = name;
            ConnectionInfo = new ConnectionInfo(key, localhost, protocol);
            Address = new Address(street, city);
            Note = note;
        }

        public PharmacyProfile(string name, string key, string localhost, ProtocolType protocol, string street, string city)
        {
            Name = name;
            ConnectionInfo = new ConnectionInfo(key, localhost, protocol);
            Address = new Address(street, city);
            Note = "";
        }

        public PharmacyProfile() { }
    }
}
