using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Model
{
    public class DoctorsSchedule
    {
        public int Id { get; set; }
        public Doctor Doctor { get; private set; }
        public Vacation Vacation { get; private set; }
        public Shift Shift { get; private set; }

        public DoctorsSchedule()
        {

        }

        public bool hasDoctorShift()
        {
            if (this.Doctor.ShiftId != null)
            {
                return true;
            }
            else return false;
        }
        public bool isDoctorAvailableForDate(DateTime date)
        {
            if (date < this.Vacation.Start && date > this.Vacation.End)
            {
                return true;
            }
            else return false;
        }

        public bool isDoctorCurentlyAvailable(DateTime date)
        {
            if (Doctor.onVacation)
            {
                return false;
            }
            else return true;
        }

    }
}
