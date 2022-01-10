using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class VacationDTO
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int DoctorId { get; set; }

        public string FullName { get; set; }

        public VacationDTO() { }
        public VacationDTO(int id, String desc, DateTime start, DateTime end, int doctorId, string fullName)
        {
            Id = id;
            Description = desc;
            Start = start;
            End = end;
            DoctorId = doctorId;
            FullName = fullName;
        }
    }
}
