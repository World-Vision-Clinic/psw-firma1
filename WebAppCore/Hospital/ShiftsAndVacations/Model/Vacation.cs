using System;
using System.Collections.Generic;
using System.Text;
using Hospital.MedicalRecords.Model;

namespace Hospital.ShiftsAndVacations.Model
{
    public class Vacation
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public int DoctorId { get; private set; }

        public Vacation() { }
        public Vacation(int id, string description, DateTime start, DateTime end, int doctor)
        {
            Id = id;
            Description = description;
            Start = start;
            End = end;
            DoctorId = doctor;
        }
    }

   
}
