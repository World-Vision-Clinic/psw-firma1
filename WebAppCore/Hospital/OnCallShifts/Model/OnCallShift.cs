using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Model
{
    public class OnCallShift
    {
        public int Id { get; private set; }
        public int DoctorId { get; private set; }
        public DateTime Date { get; private set; }
        

        public OnCallShift() { }

        public OnCallShift(int id, int doctorId, DateTime date)
        {
            Id = id;
            DoctorId = doctorId;
            Date = date;
        }
        
    }
}
