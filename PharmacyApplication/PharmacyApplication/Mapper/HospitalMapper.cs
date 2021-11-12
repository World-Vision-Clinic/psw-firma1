using Pharmacy.Model;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Mapper
{
    public class HospitalMapper
    {
        public static Hospital HospitalDtoToHospital(HospitalDto dto)
        {
            Hospital hospital = new Hospital();
            hospital.Name = dto.Name;
            hospital.Localhost = dto.Localhost;
            return hospital;
        }
    }
}
