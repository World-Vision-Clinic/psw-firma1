using System;
using System.ComponentModel;
using System.Text;

namespace Integration
{
    public class Appointment
    {
        private double durationInHours = 0.5;
        public double DurationInHours
        {
            get
            {
                return durationInHours;
            }
            set
            {
                durationInHours = value;
            }
        }

        private DateTime dateTime;
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value;
            }
        }

      

        private string iDpatient;
        public string IDpatient
        {
            get
            {
                return iDpatient;
            }
            set
            {
                iDpatient = value;
            }
        }

        private string iDDoctor;
        public string IDDoctor
        {
            get
            {
                return iDDoctor;
            }
            set
            {
                iDDoctor = value;
            }
        }

        private string iDAppointment;
        public string IDAppointment
        {
            get
            {
                return iDAppointment;
            }
            set
            {
                iDAppointment = value;
            }
        }


  


        public MedicalRecord PatientsRecord { get; set; }
      
      


        public Appointment()
        {
           
            this.PatientsRecord = new MedicalRecord();
            
        }

        public Appointment(DateTime dateTime,  string iDpatient, string iDDoctor, string iDAppointment)
        {
            this.dateTime = dateTime;
           
            this.iDpatient = iDpatient;
            this.iDDoctor = iDDoctor;
            this.iDAppointment = iDAppointment;
          
        }

        public bool IsDoctorAvaliable(Appointment appointment)
        {
            if (this.IsDoctorInAppointment(appointment.IDDoctor))
                if (this.IsOverlappingWith(appointment))
                    return false;

            return true;
        }

        public bool IsDoctorInAppointment(string doctorID)
        {
            return this.IDDoctor.Equals(doctorID);
        }

        public bool IsPatientAvaliable(Appointment appointment)
        {
            if (this.IsPatientInAppointment(appointment.IDpatient))
                if (this.IsOverlappingWith(appointment))
                    return false;

            return true;
        }

        public bool IsPatientInAppointment(string patientID)
        {
            return this.IDpatient.Equals(patientID);
        }

        public bool IsOverlappingWith(Appointment appointment)
        {
            return this.DateTime < appointment.DateTime.AddHours(appointment.DurationInHours) && appointment.DateTime < this.DateTime.AddHours(this.DurationInHours);
        }

        public bool IsOverlappingWith(DateTime startTime, DateTime endTime)
        {
            return this.DateTime < endTime && startTime < this.DateTime.AddHours(this.DurationInHours);
        }
         
        public DayOfWeek GetAppointmentsDayOfWeek()
        {
            return this.DateTime.DayOfWeek;
        }
        public string toDate()
        {
            return String.Format("{0:dd-MM-yyyy}", this.DateTime);
        }
        override
        public string ToString()
        {
            if (this.Equals(new Appointment()))
                return "";
            else
            {
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append(this.PatientsRecord.Patient.FirstName).Append(" ").Append(this.PatientsRecord.Patient.LastName);
                stringBuilder.AppendLine();
           
                stringBuilder.AppendLine();
              

                return stringBuilder.ToString();
            }
        }
    }
}