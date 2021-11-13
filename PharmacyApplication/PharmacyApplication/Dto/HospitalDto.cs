using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class HospitalDto
    {
        public string Name { get; set; }
        public string Localhost { get; set; }

        public HospitalDto() { }

        public HospitalDto(string name, string localhost)
        {
            Name = name;
            Localhost = localhost;
        }
    }
}
