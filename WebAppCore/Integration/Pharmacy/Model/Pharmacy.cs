using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    class Pharmacy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Localhost { get; set; }
        public ProtocolType Protocol { set; get; }

        public Pharmacy(string id, string name, string key, string localhost, ProtocolType protocol)
        {
            Id = id;
            Name = name;
            Key = key;
            Localhost = localhost;
            Protocol = protocol;
        }

        public Pharmacy() { }
    }
}
