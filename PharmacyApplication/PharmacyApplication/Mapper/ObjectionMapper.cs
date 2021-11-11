using Pharmacy.Model;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Mapper
{
    public class ObjectionMapper
    {
        public static Objection ObjectionDtoToObjection(ObjectionDto dto)
        {
            Objection objection = new Objection();
            objection.IdEncoded = dto.Id;
            objection.Content = dto.Content;
            return objection;
        }
    }
}
