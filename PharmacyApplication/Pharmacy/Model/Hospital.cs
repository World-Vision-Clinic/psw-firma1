using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Localhost { get; set; }

        public Hospital() { }

        public Hospital(int id, string name, string key, string localhost)
        {
            Id = id;
            Name = name;
            Key = key;
            Localhost = localhost;
        }
    }
}
