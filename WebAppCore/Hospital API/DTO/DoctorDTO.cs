using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class DoctorDTO
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int ShiftId { get; private set; }
        public int RoomId { get; private set; }
        public int Type { get; private set; }

        public bool onVacation { get; private set; }

        public DoctorDTO() { }

        public DoctorDTO(int id, string name, string surname, int s, int r, int type, bool vacation)
        {
            this.Id = id;
            this.FirstName = name;
            this.LastName = surname;
            this.ShiftId = s;
            this.RoomId = r;
            this.Type = type;
            this.onVacation = vacation;
        }
    }
}
