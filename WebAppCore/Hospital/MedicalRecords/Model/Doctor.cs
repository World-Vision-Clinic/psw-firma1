using Hospital.RoomsAndEquipment.Model;
using Hospital.ShiftsAndVacations.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public enum DoctorType
    {
        Ophthalmologist,
        Cardiologist,
        Radiologist,
        Gynecologists,
        Family_physician
    }

    public class Doctor
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int ShiftId { get; private set; }
        public int RoomId { get; private set; }
        public Doctor() { }
        public DoctorType Type { get; private set; }

        public bool onVacation { get; private set; }


        public Doctor(int id, string name, string surname, int s, int r, DoctorType type, bool vacation)
        {
            this.Validate();
            this.Id = id;
            this.FirstName = name;
            this.LastName = surname;
            this.ShiftId = s;
            this.RoomId = r;
            this.Type = type;
            this.onVacation = vacation;
        }

        public Doctor(int id, string name, string surn)
        {
            this.Id = id;
            this.FirstName = name;
            this.LastName = surn;
            this.ShiftId = -1;
            this.RoomId = -1;
            this.Type = DoctorType.Cardiologist;
        }

        public Doctor(int id, string name, string surn, DoctorType type)
        {
            this.Id = id;
            this.FirstName = name;
            this.LastName = surn;
            this.ShiftId = -1;
            this.RoomId = -1;
            this.Type = type;
        }

        public Doctor(int id)
        {
            this.Id = id;
            this.FirstName = "";
            this.LastName = "";
            this.ShiftId = -1;
            this.RoomId = -1;
            this.Type = DoctorType.Cardiologist;
        }

        public void changeShift(int shift)
        {
            this.ShiftId = shift;
        }
        public void Validate()
        {
            if (this.Id < 0)
            {
                throw new Exception();
            }
            /*if (this.ShiftId != -1)
            {
                if (this.Shift.Start < 0 || this.Shift.Start > 24)
                {
                    throw new Exception();
                }
                if (this.Shift.End < 0 || this.Shift.End > 24)
                {
                    throw new Exception();
                }
            }*/
        }
    }
}
