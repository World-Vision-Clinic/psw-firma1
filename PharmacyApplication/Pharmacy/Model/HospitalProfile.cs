using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class HospitalProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Localhost { get; set; }

        public HospitalProfile() { }

        public HospitalProfile(int id, string name, string key, string localhost)
        {
            Id = id;
            Name = name;
            Key = key;
            Localhost = localhost;
        }
    }
}
